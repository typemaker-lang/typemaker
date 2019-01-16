using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Typemaker.Ast;
using Typemaker.Ast.Validation;
using Typemaker.Compiler.Settings;

namespace Typemaker.Compiler
{
	sealed class Compiler : ICompiler
	{
		readonly IFilePathProvider filePathProvider;
		readonly ISyntaxTreeValidator syntaxTreeValidator;

		public Compiler(IFilePathProvider filePathProvider, ISyntaxTreeValidator syntaxTreeValidator)
		{
			this.filePathProvider = filePathProvider ?? throw new ArgumentNullException(nameof(filePathProvider));
			this.syntaxTreeValidator = syntaxTreeValidator ?? throw new ArgumentNullException(nameof(syntaxTreeValidator));
		}

		public async Task<ICompileResult> Compile(CodeSearchSettings settings, CancellationToken cancellationToken)
		{
			if (settings == null)
				throw new ArgumentNullException(nameof(settings));

			var parseErrors = new List<ParseError>();
			var trees = new List<ISyntaxTree>();
			var ioErrors = new List<string>();

			void CompileUnit(string path)
			{
				ISyntaxTree syntaxTree;
				IReadOnlyList<ParseError> localErrors;
				try
				{
					using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
						syntaxTree = SyntaxTreeFactory.Default.CreateSyntaxTree(fs, path, true, out localErrors);
				}
				catch (IOException)
				{
					lock (ioErrors)
						ioErrors.Add(path);
					return;
				}

				if (syntaxTree != null)
					lock (trees)
						trees.Add(syntaxTree);
				else
					lock (parseErrors)
						//we only show the first parse error because, generally, it fucks the rest of the document
						parseErrors.Add(localErrors.First());
			};

			var unitEnumerator = filePathProvider.GetCompilationUnitPaths(settings, cancellationToken);

			if (unitEnumerator == null)
				return new CompileResult();

			//use task.Run for long running ops
			await Task.WhenAll(unitEnumerator.Select(x => Task.Run(() => CompileUnit(x), cancellationToken))).ConfigureAwait(false);

			return new CompileResult(parseErrors, trees, ioErrors, syntaxTreeValidator);
		}
	}
}

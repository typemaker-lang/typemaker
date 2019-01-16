using System;
using System.Collections.Generic;
using System.IO;
using Typemaker.Ast.Visitors;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	public sealed class SyntaxTreeFactory : ISyntaxTreeFactory
	{
		public static readonly ISyntaxTreeFactory Default = new SyntaxTreeFactory(new CompilationUnitContextFactory(), new SyntaxTreeVisitorFactory());

		readonly ICompilationUnitContextFactory compilationUnitContextFactory;
		readonly ISyntaxTreeVisitorFactory syntaxTreeVisitorFactory;

		internal SyntaxTreeFactory(ICompilationUnitContextFactory compilationUnitContextFactory, ISyntaxTreeVisitorFactory syntaxTreeVisitorFactory)
		{
			this.compilationUnitContextFactory = compilationUnitContextFactory ?? throw new ArgumentNullException(nameof(compilationUnitContextFactory));
			this.syntaxTreeVisitorFactory = syntaxTreeVisitorFactory ?? throw new ArgumentNullException(nameof(syntaxTreeVisitorFactory));
		}

		public ISyntaxTree CreateSyntaxTree(Stream file, string filePath, bool disposeStream, out IReadOnlyList<ParseError> parseErrors)
		{
			var usingStream = disposeStream ? file : null;

			TypemakerParser.Compilation_unitContext compilationUnitContext;
			using (usingStream)
				compilationUnitContext = compilationUnitContextFactory.CreateCompilationUnitContext(filePath, file, out parseErrors);

			if (parseErrors.Count > 0)
				return null;

			var visitor = syntaxTreeVisitorFactory.CreateSyntaxTreeVisitor(filePath);

			var tree = visitor.ConstructSyntaxTree(compilationUnitContext);

			return tree;
		}
	}
}

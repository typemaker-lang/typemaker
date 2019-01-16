using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Typemaker.Ast;
using Typemaker.Ast.Validation;

namespace Typemaker.Compiler
{
	sealed class CompileResult : ICompileResult
	{
		public IReadOnlyList<IValidSyntaxTree> ValidSyntaxTrees { get; }

		public IReadOnlyList<CompilerError> Errors { get; }

		public CompileResult()
		{
			ValidSyntaxTrees = new List<IValidSyntaxTree>();
			Errors = new List<CompilerError>
			{
				new CompilerError
				{
					Code = "TL0001",
					ErrorClass = ErrorClass.Error,
					Message = "An IO error was encountered while enumerating compilation units!"
				}
			};
		}

		public CompileResult(IEnumerable<ParseError> parseErrors, IEnumerable<ISyntaxTree> syntaxTrees, IEnumerable<string> ioErrorPaths, ISyntaxTreeValidator syntaxTreeValidator)
		{
			if (parseErrors == null)
				throw new ArgumentNullException(nameof(parseErrors));
			if (syntaxTrees == null)
				throw new ArgumentNullException(nameof(syntaxTrees));
			if (ioErrorPaths == null)
				throw new ArgumentNullException(nameof(ioErrorPaths));
			if (syntaxTreeValidator == null)
				throw new ArgumentNullException(nameof(syntaxTreeValidator));

			var errors = new List<CompilerError>();


			foreach (var I in ioErrorPaths)
				errors.Add(new CompilerError
				{
					Code = "TL0002",
					ErrorClass = ErrorClass.Error,
					FilePath = I,
					Message = "An IO error occurred while accessing the file"
				});

			foreach (var I in parseErrors)
				errors.Add(new CompilerError
				{
					Code = "TPXXXX",
					ErrorClass = ErrorClass.Error,
					FilePath = I.FilePath,
					Location = new Highlight
					{
						Start = I.Location,
						End = new Location
						{
							Line = I.Location.Line + 1,
							Column = 0
						}
					},
					Message = I.Description
				});

			var trees = new List<IValidSyntaxTree>();
			foreach(var I in syntaxTrees)
			{
				var validationResult = syntaxTreeValidator.ValidateSyntaxTree(I);
				if (validationResult.SyntaxTree != null)
					trees.Add(validationResult.SyntaxTree);
				else
					foreach (var J in validationResult.Errors)
						errors.Add(new CompilerError
						{
							ErrorClass = ErrorClass.Error,
							FilePath = I.FilePath,
							Location = J.Location,
							Message = J.Description,
							Code = String.Format(CultureInfo.InvariantCulture, "TV{0}", ((int)J.Code).ToString("X4"))
						});
			}

			Errors = errors;
			ValidSyntaxTrees = trees;
		}
	}
}
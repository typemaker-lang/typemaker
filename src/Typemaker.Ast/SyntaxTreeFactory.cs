using System.Collections.Generic;
using System.IO;
using Typemaker.Ast.Visitors;

namespace Typemaker.Ast
{
	public static class SyntaxTreeFactory
	{
		internal static ICompilationUnitContextFactory compilationUnitContextFactory = new CompilationUnitContextFactory();
		internal static ISyntaxTreeVisitorFactory syntaxTreeVisitorFactory = new SyntaxTreeVisitorFactory();

		public static ISyntaxTree CreateSyntaxTree(Stream file, string filePath, out IReadOnlyList<ParseError> parseErrors)
		{
			var compilationUnitContext = compilationUnitContextFactory.CreateCompilationUnitContext(file, out var tokensAccessor, out parseErrors);

			if (parseErrors.Count > 0)
				return null;

			var visitor = syntaxTreeVisitorFactory.CreateSyntaxTreeVisitor(filePath);

			var tree = visitor.Visit(compilationUnitContext);
			var tokens = tokensAccessor();
			tree.Build(tokens);

			return tree;
		}
	}
}

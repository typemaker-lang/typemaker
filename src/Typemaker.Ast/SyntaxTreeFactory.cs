using System;
using System.Collections.Generic;
using System.IO;
using Typemaker.Ast.Visitors;

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

		public ISyntaxTree CreateSyntaxTree(Stream file, string filePath, out IReadOnlyList<ParseError> parseErrors)
		{
			var compilationUnitContext = compilationUnitContextFactory.CreateCompilationUnitContext(file, out var tokensAccessor, out parseErrors);

			if (parseErrors.Count > 0)
				return null;

			var visitor = syntaxTreeVisitorFactory.CreateSyntaxTreeVisitor(filePath);

			var tree = visitor.ConstructSyntaxTree(compilationUnitContext);
			var tokens = tokensAccessor();
			tree.Build(tokens);

			return tree;
		}
	}
}

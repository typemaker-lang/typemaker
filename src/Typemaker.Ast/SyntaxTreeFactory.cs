using Antlr4.Runtime;
using System.Collections.Generic;
using System.IO;
using Typemaker.Ast.Visitors;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	public static class SyntaxTreeFactory
	{
		public static ISyntaxTree CreateSyntaxTree(Stream file, string filePath, out IReadOnlyList<ParseError> parseErrors)
		{
			var input = new AntlrInputStream(file);
			var lexer = new TypemakerLexer(input);

			var errors = new List<ParseError>();
			var errorListener = new ReportingErrorListener(errors, lexer.Vocabulary);
			lexer.AddErrorListener(errorListener);

			var tokenStream = new CommonTokenStream(lexer);
			var parser = new TypemakerParser(tokenStream);
			parser.AddErrorListener(errorListener);

			var compilationUnitContext = parser.compilation_unit();

			parseErrors = errors;
			if (errors.Count > 0)
				return null;

			var visitor = new CompilationUnitVisitor(filePath);
			var tree = visitor.Visit(compilationUnitContext);
			var tokens = tokenStream.GetTokens();
			tree.Build(tokens);

			return tree;
		}
	}
}

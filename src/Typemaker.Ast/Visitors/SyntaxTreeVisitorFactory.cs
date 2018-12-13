using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	sealed class SyntaxTreeVisitorFactory : ISyntaxTreeVisitorFactory
	{
		public ITypemakerParserVisitor<SyntaxTree> CreateSyntaxTreeVisitor(string filePath) => new CompilationUnitVisitor(filePath);
	}
}

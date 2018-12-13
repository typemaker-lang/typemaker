using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	interface ISyntaxTreeVisitorFactory
	{
		ITypemakerParserVisitor<SyntaxTree> CreateSyntaxTreeVisitor(string filePath);
	}
}
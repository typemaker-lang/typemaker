using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	interface ISyntaxTreeVisitorFactory
	{
		ISyntaxTreeVisitor CreateSyntaxTreeVisitor(string filePath);
	}
}
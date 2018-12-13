using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	interface ISyntaxTreeVisitor
	{
		SyntaxTree Visit(TypemakerParser.Compilation_unitContext context);
	}
}
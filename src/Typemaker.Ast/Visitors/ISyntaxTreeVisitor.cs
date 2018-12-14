using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	interface ISyntaxTreeVisitor
	{
		SyntaxTree ConstructSyntaxTree(TypemakerParser.Compilation_unitContext context);
	}
}
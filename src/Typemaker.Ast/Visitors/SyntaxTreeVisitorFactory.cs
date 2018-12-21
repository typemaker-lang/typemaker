using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	sealed class SyntaxTreeVisitorFactory : ISyntaxTreeVisitorFactory
	{
		public ISyntaxTreeVisitor CreateSyntaxTreeVisitor(string filePath) => new TypemakerVisitor(filePath);
	}
}

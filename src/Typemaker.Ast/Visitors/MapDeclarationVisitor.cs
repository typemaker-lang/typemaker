using Antlr4.Runtime.Misc;
using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	sealed class MapDeclarationVisitor : TypemakerNodeVisitor
	{
		public override SyntaxNode VisitMap([NotNull] TypemakerParser.MapContext context) => new MapDeclaration(context.GetToken(TypemakerLexer.RES, 0).Symbol, context);
	}
}
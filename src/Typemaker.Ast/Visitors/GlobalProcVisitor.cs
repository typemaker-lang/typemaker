using Antlr4.Runtime.Misc;
using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	sealed class GlobalProcVisitor : TypemakerVisitor
	{
		public override SyntaxNode VisitGlobal_proc([NotNull] TypemakerParser.Global_procContext context)
		{
			return base.VisitGlobal_proc(context);
		}
	}
}
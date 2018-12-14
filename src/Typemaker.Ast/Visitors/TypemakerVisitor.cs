using Antlr4.Runtime;
using System.Collections.Generic;
using System.Linq;
using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	abstract class TypemakerVisitor : TypemakerParserBaseVisitor<SyntaxNode>, ITypemakerVisitor
	{
		protected IEnumerable<SyntaxNode> Visit<TContext>(TContext[] contexts)
			where TContext : ParserRuleContext => contexts.Select(context => Visit(context));
		protected IEnumerable<SyntaxNode> VisitWith<TContext, TVisitor>(TContext[] contexts, TVisitor visitor)
			where TContext : ParserRuleContext
			where TVisitor : TypemakerVisitor => contexts.Select(context => visitor.Visit(context));

		protected static IEnumerable<SyntaxNode> ConcatNodes(params IEnumerable<SyntaxNode>[] nodes)
		{
			var tmp = nodes[0];
			for (var I = 1; I < nodes.Length; ++I)
				tmp = Enumerable.Concat(tmp, nodes[I]);
			return tmp;
		}
	}
}
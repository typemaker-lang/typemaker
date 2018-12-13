using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	abstract class TypemakerVisitor<TNode> : TypemakerParserBaseVisitor<TNode> where TNode : SyntaxNode
	{
		protected static IEnumerable<SyntaxNode> VisitWith<TVisitor, TContext>(TContext[] contexts)
			where TVisitor : TypemakerNodeVisitor, new()
			where TContext : ParserRuleContext
		{
			if (contexts == null)
				throw new ArgumentNullException(nameof(contexts));
			var visitor = new TVisitor();
			return contexts.Select(context => visitor.Visit(context));
		}

		protected static IEnumerable<SyntaxNode> ConcatNodes(params IEnumerable<SyntaxNode>[] nodes)
		{
			if (nodes == null)
				throw new ArgumentNullException(nameof(nodes));
			if (nodes.Length == 0)
				throw new ArgumentOutOfRangeException(nameof(nodes));
			var tmp = nodes[0];
			for (var I = 1; I < nodes.Length; ++I)
				tmp = Enumerable.Concat(tmp, nodes[I]);
			return tmp;
		}
	}
}
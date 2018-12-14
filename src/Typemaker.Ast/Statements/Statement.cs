using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typemaker.Ast.Statements
{
	abstract class Statement : SyntaxNode, IStatement
	{
		public bool BlockBreaker { get; }
		public virtual bool HasSideEffects => SelectChildren<IStatement>().Any(x => HasSideEffects);

		protected Statement(ParserRuleContext context, IEnumerable<SyntaxNode> children, bool blockBreaker) : base(context, children)
		{
			BlockBreaker = blockBreaker;
		}
	}
}

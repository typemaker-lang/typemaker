using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typemaker.Ast.Serialization;

namespace Typemaker.Ast.Statements
{
	abstract class Statement : SyntaxNode, IStatement
	{
		public bool BlockBreaker { get; }
		public abstract bool HasSideEffects { get; }
		protected Statement(ParserRuleContext context, IEnumerable<SyntaxNode> children, bool blockBreaker) : base(context, children)
		{
			BlockBreaker = blockBreaker;
		}
	}
}

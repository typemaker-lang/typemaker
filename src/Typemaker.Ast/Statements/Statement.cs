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
		public abstract bool HasSideEffects { get; }
		protected Statement(IEnumerable<ITrivia> children, bool blockBreaker) : base(children)
		{
			BlockBreaker = blockBreaker;
		}
	}
}

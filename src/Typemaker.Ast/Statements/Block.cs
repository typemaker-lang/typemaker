using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typemaker.Parser;

namespace Typemaker.Ast.Statements
{
	sealed class Block : Statement, IBlock
	{
		public bool Unsafe { get; }

		public IEnumerable<IStatement> Statements => ChildrenAs<IStatement>();

		public override bool HasSideEffects => ChildrenAs<IStatement>().Any(x => x.HasSideEffects);
		
		public Block(bool isUnsafe, IEnumerable<ITrivia> children) : base(children, false)
		{
			Unsafe = isUnsafe;
		}
	}
}

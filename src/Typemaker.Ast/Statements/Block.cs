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
		
		public Block(TypemakerParser.BlockContext context, IEnumerable<IInternalTrivia> children) : base(context, children, false) { }
		public Block(TypemakerParser.Unsafe_blockContext context, IEnumerable<IInternalTrivia> children) : base(context, children, false)
		{
			Unsafe = true;
		}
	}
}

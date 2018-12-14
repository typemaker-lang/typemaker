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

		public IReadOnlyList<IStatement> Statements => ChildrenAs<IStatement>();

		public override bool HasSideEffects => SelectChildren<IStatement>().Any(x => x.HasSideEffects);

		public Block(TypemakerParser.BlockContext context, IEnumerable<SyntaxNode> children) : base(context, children, false) { }
		public Block(TypemakerParser.Unsafe_blockContext context, IEnumerable<SyntaxNode> children) : base(context, children, false)
		{
			Unsafe = true;
		}
	}
}

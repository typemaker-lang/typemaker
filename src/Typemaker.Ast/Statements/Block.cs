using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Parser;

namespace Typemaker.Ast.Statements
{
	sealed class Block : Statement, IBlock
	{
		public bool Unsafe { get; }

		public IReadOnlyList<IStatement> Statements => ChildrenAs<IStatement>();

		public Block(TypemakerParser.BlockContext context, IEnumerable<SyntaxNode> children) : base(context, children, false) { }
		public Block(TypemakerParser.Unsafe_blockContext context, IEnumerable<SyntaxNode> children) : base(context, children, false)
		{
			Unsafe = true;
		}
	}
}

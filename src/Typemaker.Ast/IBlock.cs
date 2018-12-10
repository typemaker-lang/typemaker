using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	interface IBlock : ISyntaxNode
	{
		bool IsUnsafe { get; }
	}
}

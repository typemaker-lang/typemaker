using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IWhitespaceTrivia : ISyntaxNode
	{
		WhitespaceType Type { get; }
		ulong Amount { get; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface ITrivia : ILocatable
	{
		IToken Token { get; }
		ISyntaxNode Node { get; }
	}
}

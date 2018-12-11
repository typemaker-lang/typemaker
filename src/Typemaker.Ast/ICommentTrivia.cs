using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface ICommentTrivia : ISyntaxNode
	{
		ulong? LineCount { get; }
		string Comment { get; }
	}
}

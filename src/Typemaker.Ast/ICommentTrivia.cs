using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface ICommentTrivia : ITrivia
	{
		string Comment { get; }
	}
}

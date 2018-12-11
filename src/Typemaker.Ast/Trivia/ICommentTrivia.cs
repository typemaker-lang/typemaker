using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Trivia
{
	public interface ICommentTrivia : ITrivia
	{
		string Comment { get; }
	}
}

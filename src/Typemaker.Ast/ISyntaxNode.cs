using System;
using System.Collections.Generic;

namespace Typemaker.Ast
{
	public interface ISyntaxNode : ITrivia
	{
		ISyntaxTree Tree { get; }
		ISyntaxNode Parent { get; }

		ILocation Start { get; }
		ILocation End { get; }

		IReadOnlyList<ISyntaxNode> Children { get; }

		string Syntax { get; }
	}
}

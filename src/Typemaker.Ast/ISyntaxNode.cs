using System.Collections.Generic;
using Typemaker.Ast.Trivia;

namespace Typemaker.Ast
{
	public interface ISyntaxNode
	{
		ILocation Start { get; }
		ILocation End { get; }

		IReadOnlyList<ICommentTrivia> Comments { get; }
		IReadOnlyList<IWhitespaceTrivia> Whitespace { get; }

		ISyntaxTree Tree { get; }
		ISyntaxNode Parent { get; }

		IReadOnlyList<ISyntaxNode> Children { get; }

		bool Trivia { get; }
	}
}

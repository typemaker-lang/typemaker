using System.Collections.Generic;
using Typemaker.Ast.Serialization;

namespace Typemaker.Ast
{
	public interface ISyntaxNode
	{
		Location Start { get; }
		Location End { get; }

		IReadOnlyList<ICommentTrivia> Comments { get; }
		IReadOnlyList<IWhitespaceTrivia> Whitespace { get; }

		ISyntaxTree Tree { get; }
		ISyntaxNode Parent { get; }

		IReadOnlyList<ISyntaxNode> Children { get; }

		bool Trivia { get; }

		SyntaxGraph Serialize();

		void Transform(IEnumerable<SyntaxGraph> replacements);
	}
}

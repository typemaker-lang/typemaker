using System.Collections.Generic;
using Typemaker.Ast.Serialization;

namespace Typemaker.Ast
{
	public interface ISyntaxNode : ILocatable
	{
		IReadOnlyList<ICommentTrivia> Comments { get; }
		IReadOnlyList<IWhitespaceTrivia> Whitespace { get; }

		ISyntaxTree Tree { get; }
		ISyntaxNode Parent { get; }

		IReadOnlyList<ISyntaxNode> Children { get; }

		bool Trivia { get; }

		ILocatable TriviaRestrictionViolation { get; }

		SyntaxGraph Serialize();

		void Transform(IEnumerable<SyntaxGraph> replacements);

		IEnumerable<TChildNode> SelectChildren<TChildNode>() where TChildNode : ISyntaxNode;
	}
}

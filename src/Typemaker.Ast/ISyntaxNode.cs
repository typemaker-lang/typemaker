using System.Collections.Generic;

namespace Typemaker.Ast
{
	public interface ISyntaxNode : ILocatable
	{
		ISyntaxTree Tree { get; }
		ISyntaxNode Parent { get; }

		IEnumerable<IToken> Tokens { get; }

		IEnumerable<ISyntaxNode> Children { get; }

		IReadOnlyList<ITrivia> Trivia { get; }

		IEnumerable<TChildNode> ChildrenAs<TChildNode>() where TChildNode : ISyntaxNode;

		void LinkTree(ISyntaxNode parent, ISyntaxTree tree);
	}
}

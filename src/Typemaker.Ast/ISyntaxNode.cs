using System.Collections.Generic;

namespace Typemaker.Ast
{
	public interface ISyntaxNode : ILocatable
	{
		ISyntaxTree Tree { get; }
		ISyntaxNode Parent { get; }

		IEnumerable<ISyntaxNode> Children { get; }

		IEnumerable<ITrivia> Trivia { get; }

		IEnumerable<TChildNode> ChildrenAs<TChildNode>() where TChildNode : ISyntaxNode;
	}
}

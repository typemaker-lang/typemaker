using System.Collections.Generic;
using Typemaker.Ast.Trivia;

namespace Typemaker.Ast
{
	public interface ISyntaxNode : ITrivia
	{
		ISyntaxTree Tree { get; }
		ISyntaxNode Parent { get; }

		IReadOnlyList<ISyntaxNode> Children { get; }

		string Syntax { get; }
	}
}

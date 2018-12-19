using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast;

namespace Typemaker.Anaylsis
{
	public interface IAnaylzer
	{
		IEnumerable<AnaylsisResult> AnaylzeNode(ISyntaxNode node, ISyntaxNode leftNode, ISyntaxNode rightNode, IEnumerable<ISyntaxNode> leftTrivia, IEnumerable<ISyntaxNode> rightTrivia);
	}
}

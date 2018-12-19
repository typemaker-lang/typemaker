using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typemaker.Ast;

namespace Typemaker.Anaylsis.Linting
{
	public sealed class TopLevelOrderingAnalyzer : IAnaylzer
	{
		public IEnumerable<AnaylsisResult> AnaylzeNode(ISyntaxNode node, ISyntaxNode leftNode, ISyntaxNode rightNode, IEnumerable<ISyntaxNode> leftTrivia, IEnumerable<ISyntaxNode> rightTrivia)
		{
			if (node.Parent != node.Tree)
				yield break;

			if (node is IMapDeclaration map)
			{
				if (leftNode != null) {
					var transform = node.Tree.Children.Where(x => x != node).ToList();
					transform.Insert(0, node);
					yield return new AnaylsisResult
					{
						Code = 1,
						End = node.End,
						Start = node.Start,
						Highlight = true,
						Message = "Map declarations should be first in a file",
						Suggestions = new List<SyntaxTransformation>
						{
							new SyntaxTransformation
							{
								Description = "Move to top of file",
								Target = node.Tree,
								Transformation = transform
							}
						}
					};
				}
			}
			else if (node is IVarDeclaration var)
			{
				if(leftNode != null && !(leftNode is IVarDeclaration) && !(leftNode is IMapDeclaration))
					yield return new AnaylsisResult
					{
						Code = 2,
						End = node.End,
						Start = node.Start,
						Highlight = true,
						Message = "Global var declarations should come after map declarations"
					};
			}
			else if (node is IInterface inter || node is IEnumDefinition enu)
			{
				if (leftNode != null && !(leftNode is IEnumDefinition) && !(leftNode is IInterface) && !(leftNode is IVarDeclaration))
					yield return new AnaylsisResult
					{
						Code = 2,
						End = node.End,
						Start = node.Start,
						Highlight = true,
						Message = "Enum/Interface declarations should come after var declarations"
					};
			}
			//TODO
		}
	}
}

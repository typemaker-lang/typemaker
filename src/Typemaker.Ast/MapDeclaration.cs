using System;
using System.Linq;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class MapDeclaration : SyntaxNode, IMapDeclaration
	{
		public string MapPath { get; }
		public MapDeclaration(TypemakerParser.MapContext context) : base(context, Enumerable.Empty<SyntaxNode>())
		{
			MapPath = ParseTreeFormatters.ExtractResource(context.RES());
		}
	}
}

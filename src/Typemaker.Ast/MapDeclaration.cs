using System;
using System.Collections.Generic;
using System.Linq;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class MapDeclaration : SyntaxNode, IMapDeclaration
	{
		public string MapPath { get; }
		public MapDeclaration(TypemakerParser.MapContext context, IEnumerable<IInternalTrivia> children) : base(context, children)
		{
			MapPath = ParseTreeFormatters.ExtractResource(context.RES());
		}
	}
}

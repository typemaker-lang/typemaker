using System;
using System.Collections.Generic;

namespace Typemaker.Ast
{
	sealed class MapDeclaration : SyntaxNode, IMapDeclaration
	{
		public string MapPath { get; }
		public MapDeclaration(string mapPath, IEnumerable<ITrivia> children) : base(children)
		{
			MapPath = mapPath ?? throw new ArgumentNullException(nameof(mapPath));
		}
	}
}

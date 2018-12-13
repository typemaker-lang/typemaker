using Antlr4.Runtime;
using System;
using System.Linq;

namespace Typemaker.Ast
{
	sealed class MapDeclaration : SyntaxNode, IMapDeclaration
	{
		public string MapPath { get; }
		public MapDeclaration(IToken resource, ParserRuleContext context) : base(context, Enumerable.Empty<SyntaxNode>())
		{
			MapPath = TokenFormatters.ExtractResource(resource ?? throw new ArgumentNullException(nameof(resource)));
		}
	}
}

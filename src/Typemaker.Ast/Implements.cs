using System.Collections.Generic;
using System.Linq;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class Implements : SyntaxNode, IImplements
	{
		public string Name { get; }

		public Implements(TypemakerParser.Implements_statementContext context) : base(context, Enumerable.Empty<SyntaxNode>())
		{
			Name = ParseTreeFormatters.ExtractIdentifier(context.IDENTIFIER());
		}
	}
}

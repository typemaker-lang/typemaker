using System.Collections.Generic;
using System.Linq;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class Implements : SyntaxNode, IImplements
	{
		public string Name { get; }

		public Implements(TypemakerParser.Implements_statementContext context, IEnumerable<IInternalTrivia> children) : base(context, children)
		{
			Name = ParseTreeFormatters.ExtractIdentifier(context.IDENTIFIER());
		}
	}
}

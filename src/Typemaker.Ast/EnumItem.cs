using System.Collections.Generic;
using System.Linq;
using Typemaker.Ast.Statements.Expressions;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class EnumItem : SyntaxNode, IEnumItem
	{
		public string Name { get; }

		public bool AutoValue { get; }

		public IExpression Expression => SelectChildren<IExpression>().FirstOrDefault();

		public EnumItem(TypemakerParser.Enum_itemContext context, SyntaxNode initializer) : base(context, initializer != null ? new List<SyntaxNode>{ initializer } : Enumerable.Empty<SyntaxNode>())
		{
			Name = ParseTreeFormatters.ExtractIdentifier(context.IDENTIFIER());

			AutoValue = !(context.ChildCount > 1);
		}
	}
}

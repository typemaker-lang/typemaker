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

		public IExpression Expression => ChildrenAs<IExpression>().FirstOrDefault();

		public EnumItem(TypemakerParser.Enum_itemContext context, IEnumerable<ITrivia> children) : base(children)
		{
			Name = ParseTreeFormatters.ExtractIdentifier(context.IDENTIFIER());

			AutoValue = !(context.ChildCount > 1);
		}
	}
}

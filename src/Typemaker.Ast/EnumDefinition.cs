using System.Collections.Generic;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	/// <inheritdoc />
	sealed class EnumDefinition : SyntaxNode, IEnumDefinition
	{
		/// <inheritdoc />
		public string Name { get; }

		/// <inheritdoc />
		public IReadOnlyList<IEnumItem> Items => ChildrenAs<IEnumItem>();

		/// <summary>
		/// Construct an <see cref="EnumDefinition"/>
		/// </summary>
		/// <param name="context">The <see cref="TypemakerParser.EnumContext"/></param>
		/// <param name="children">The child <see cref="SyntaxNode"/>s</param>
		public EnumDefinition(TypemakerParser.EnumContext context, IEnumerable<SyntaxNode> children) : base(context, children)
		{
			var enumType = context.enum_type();
			Name = enumType.IDENTIFIER().Symbol.Text;
			AntiTriviaContext(enumType);
		}
	}
}

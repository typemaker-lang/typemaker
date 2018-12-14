using System.Collections.Generic;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class NullableType : TrueType, INullableType
	{
		public bool IsNullable { get; }

		public NullableType(TypemakerParser.Nullable_typeContext context, IEnumerable<SyntaxNode> children) : base(context.true_type(), children)
		{
			IsNullable = context.NULLABLE() != null;
		}
	}
}

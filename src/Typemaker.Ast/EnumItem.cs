﻿using System.Collections.Generic;
using System.Linq;
using Typemaker.Ast.Expressions;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class EnumItem : SyntaxNode, IEnumItem
	{
		public string Name { get; }

		public bool AutoValue { get; }

		public IExpression Expression => ChildAs<IExpression>();

		public EnumItem(TypemakerParser.Enum_itemContext context, SyntaxNode initializer) : base(context, initializer != null ? new List<SyntaxNode>{ initializer } : new List<SyntaxNode>())
		{
			Name = ParseTreeFormatters.ExtractIdentifier(context.IDENTIFIER());

			AutoValue = !(context.ChildCount > 1);
		}
	}
}
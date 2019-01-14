using System;
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

		public EnumItem(string name, bool autoValue, IEnumerable<ITrivia> children) : base(children)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			AutoValue = autoValue;
		}
	}
}

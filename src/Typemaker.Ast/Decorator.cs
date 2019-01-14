using System;
using System.Collections.Generic;
using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast
{
	sealed class Decorator : SyntaxNode, IDecorator
	{
		public DecoratorType Type { get; }

		public IIntegerExpression Precedence => ChildAs<IIntegerExpression>();

		public bool? PublicProtection { get; }

		Decorator(DecoratorType type, IEnumerable<ITrivia> children, bool restrictTypes) : base(children)
		{
			if (restrictTypes && type == DecoratorType.Protection)
				throw new ArgumentOutOfRangeException(nameof(type), type, "Use the other constructor for protection decorators!");
			Type = type;
		}

		public Decorator(bool publicProtection, IEnumerable<ITrivia> children) : this(DecoratorType.Protection, children, false)
		{
			PublicProtection = publicProtection;
		}

		public Decorator(DecoratorType type, IEnumerable<ITrivia> children) : this(type, children, true)
		{
		}
	}
}

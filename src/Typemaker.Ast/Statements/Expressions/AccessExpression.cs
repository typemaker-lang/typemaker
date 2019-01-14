using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Parser;

namespace Typemaker.Ast.Statements.Expressions
{
	sealed class AccessExpression : Expression, IAccessExpression
	{
		public override bool HasSideEffects => false;

		public bool Optional { get; }

		public bool Unsafe { get; }

		public string Name { get; }

		public IExpression Target => ChildAs<IExpression>();

		public override RootType? EvaluateType() => null;

		public AccessExpression(TypemakerParser.Accessed_targetContext context, IEnumerable<IInternalTrivia> children) : base(context, children)
		{
			Name = ParseTreeFormatters.ExtractIdentifier(context.IDENTIFIER());

			var accessor = context.accessor();
			Optional = accessor.QUESTION() != null;
			Unsafe = accessor.nonoptional_accessor().COLON() != null;
		}
	}
}

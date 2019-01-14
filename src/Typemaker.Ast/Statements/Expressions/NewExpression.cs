using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Parser;

namespace Typemaker.Ast.Statements.Expressions
{
	sealed class NewExpression : Expression, INewExpression
	{
		public override bool IsConstant => false;

		public override bool HasSideEffects => true;

		public override bool IsCompileTime => true;

		public IExpression AccessedType => ChildAs<IExpression>();

		public IEnumerable<IArgument> Arguments => ChildrenAs<IArgument>();

		public override RootType? EvaluateType() => RootType.Object;

		public NewExpression(TypemakerParser.New_expressionContext context, IEnumerable<ITrivia> children): base(children)
		{
		}
	}
}

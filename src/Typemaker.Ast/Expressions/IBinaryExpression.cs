using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Expressions
{
	interface IBinaryExpression : IExpression
	{
		IExpression LeftHandSide { get; }

		BinaryOperator Operator { get; }

		IExpression RightHandSide { get; }
	}
}

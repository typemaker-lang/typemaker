using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	interface IBinaryExpression : IExpression
	{
		IExpression LeftHandSide { get; }

		BinaryOperator Operator { get; }

		IExpression RightHandSide { get; }
	}
}

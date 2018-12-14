using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	public enum BinaryOperator
	{
		Add,
		Subtract,
		Multiply,
		Divide,
		Exponent,
		Modulo,
		Assign,
		NotAssign,
		Less,
		Greater,
		LessOrEqual,
		GreatorOrEqual,
		Equivalence,
		Inequivalence,
		BitwiseAnd,
		BitwiseOr,
		BitwiseXor,
		LeftShift,
		RightShift,
		LogicalAnd,
		LogicalOr,
		In,
		To
	}
}

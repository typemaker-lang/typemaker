using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	public enum UnaryOperator
	{
		Negation,
		Not,
		BitwiseNot,
		PreIncrement,
		PreDecrement,
		PostIncrement,
		PostDecrement
	}
}

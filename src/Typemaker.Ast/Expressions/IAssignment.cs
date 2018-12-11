using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Expressions
{
	public interface IAssignment : IExpression
	{
		IExpression LeftHandSide { get; }
		AssignmentOperator Operator { get; }
		IExpression RightHandSide { get; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Expressions;

namespace Typemaker.Ast.Statements
{
	public interface IAssignmentStatement : IStatementExpression
	{
		IExpression LeftHandSide { get; }
		AssignmentOperator Operator { get; }
		IExpression RightHandSide { get; }
	}
}

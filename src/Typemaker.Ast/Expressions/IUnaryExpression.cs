using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Statements;

namespace Typemaker.Ast.Expressions
{
	public interface IUnaryExpression : IStatementExpression
	{
		IExpression Interior { get; }
		UnaryOperator Operator { get; }
	}
}

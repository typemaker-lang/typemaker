using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	public interface IUnaryExpression : IExpression
	{
		IExpression Interior { get; }
		UnaryOperator Operator { get; }
	}
}

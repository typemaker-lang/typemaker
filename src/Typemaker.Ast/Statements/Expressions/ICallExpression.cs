using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	public interface ICallExpression : IInvocationExpression
	{
		IExpression Function { get; }
	}
}

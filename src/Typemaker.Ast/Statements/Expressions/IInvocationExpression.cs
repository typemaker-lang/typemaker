using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	public interface IInvocationExpression : IExpression
	{
		bool IsNewExpression { get; }
		IExpression Target { get; }
		IEnumerable<IArgument> Arguments { get; }
	}
}

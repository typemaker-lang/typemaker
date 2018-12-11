using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Expressions
{
	public interface IInvocationExpression : IStatementExpression
	{
		bool IsNewExpression { get; }
		IExpression Target { get; }
		IReadOnlyList<IExpression> PositionalArguments { get; }
		IReadOnlyDictionary<string, IExpression> NamedArguments { get; }
	}
}

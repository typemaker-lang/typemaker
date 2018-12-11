using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Expressions
{
	interface IInputExpression : IExpression
	{
		IExpression Target { get; }
		IExpression Message { get; }
		IExpression Title { get; }
		IExpression Default { get; }
		INullableType ReturnType { get; }
		IExpression In { get; }
	}
}

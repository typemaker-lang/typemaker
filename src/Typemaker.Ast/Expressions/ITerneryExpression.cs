using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Expressions
{
	interface ITerneryExpression : IExpression
	{
		IExpression If { get; }
		IExpression Then { get; }
		IExpression Else { get; }
	}
}

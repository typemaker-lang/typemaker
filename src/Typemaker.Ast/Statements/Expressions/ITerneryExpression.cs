using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	interface ITerneryExpression : IExpression
	{
		IExpression If { get; }
		IExpression Then { get; }
		IExpression Else { get; }
	}
}

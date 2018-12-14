using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	interface IBooleanExpression : IExpression
	{
		bool True { get; }
	}
}

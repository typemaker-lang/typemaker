using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Expressions
{
	interface IBooleanExpression : IExpression
	{
		bool True { get; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	interface IBracketedExpression
	{
		IExpression Interior { get; }
	}
}

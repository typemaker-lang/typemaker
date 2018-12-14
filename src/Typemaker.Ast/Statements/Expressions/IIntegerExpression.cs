using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	public interface IIntegerExpression : IExpression
	{
		int Value { get; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Expressions;

namespace Typemaker.Ast.Statements
{
	public interface IReturnStatement : IControlFlowStatement
	{
		IExpression Result { get; }
	}
}

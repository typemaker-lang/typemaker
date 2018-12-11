using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Expressions;
using Typemaker.Ast.Statements;

namespace Typemaker.Ast
{
	public interface IStatementExpression : IStatement, IExpression
	{
	}
}

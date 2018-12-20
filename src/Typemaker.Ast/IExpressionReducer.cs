using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast
{
	public interface IExpressionReducer
	{
		TRootType Reduce<TRootType>(IExpression expression);
	}
}

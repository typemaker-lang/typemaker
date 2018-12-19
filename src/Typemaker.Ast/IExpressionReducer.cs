using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast
{
	public interface IExpressionReducer
	{
		TRootType ReduceR<TRootType>(IExpression expression) where TRootType : class;
		TRootType? ReduceS<TRootType>(IExpression expression) where TRootType : struct;
	}
}

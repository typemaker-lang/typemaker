using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	public interface IExpression : IStatement
	{
		bool SideEffects { get; }

		RootType? EvaluateType();
	}
}

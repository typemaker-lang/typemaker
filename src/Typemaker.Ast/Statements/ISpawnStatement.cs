using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast.Statements
{
	public interface ISpawnStatement : IBodiedStatement
	{
		IExpression Duration { get; }
	}
}

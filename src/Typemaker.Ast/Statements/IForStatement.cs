using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements
{
	public interface IForStatement
	{
		IVarDefinition Var { get; }
		IStatement Body { get; }
	}
}

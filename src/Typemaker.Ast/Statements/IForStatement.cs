using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements
{
	public interface IForStatement
	{
		IVarDeclaration Var { get; }
		IStatement Body { get; }
	}
}

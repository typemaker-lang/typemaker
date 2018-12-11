using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements
{
	public interface IControlFlowStatement : IStatement
	{
		ControlFlowType Type { get; }
	}
}

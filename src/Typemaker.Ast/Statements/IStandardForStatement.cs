using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements
{
	public interface IStandardForStatement : IForStatement, IBranch
	{
		IStatement StepAction { get; }
	}
}

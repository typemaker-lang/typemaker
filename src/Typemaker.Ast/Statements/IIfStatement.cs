using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements
{
	public interface IIfStatement : IIfBase
	{
		IReadOnlyList<IIfBase> ElseIfs { get; }
		IStatement Else { get; }
	}
}

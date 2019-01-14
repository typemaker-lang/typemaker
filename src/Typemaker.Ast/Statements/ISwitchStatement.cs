using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements
{
	public interface ISwitchStatement : IBranch
	{
		IEnumerable<IIfBase> Cases { get; }

		IStatement Default { get; }
	}
}

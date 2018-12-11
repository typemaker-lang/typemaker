using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements
{
	public interface IWhileStatement : IIfBase
	{
		bool Do { get; }
	}
}

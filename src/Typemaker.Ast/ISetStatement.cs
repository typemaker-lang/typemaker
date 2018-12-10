using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IAssignStatement : ISyntaxNode
	{
		bool IsSet { get; }
	}
}

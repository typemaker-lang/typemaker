using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IImplementsStatement : ISyntaxNode
	{
		string InterfaceName { get; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IImplements : ISyntaxNode
	{
		string InterfaceName { get; }
	}
}

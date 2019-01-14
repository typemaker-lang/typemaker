using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IDecorated : ISyntaxNode
	{
		IEnumerable<IDecorator> Decorators { get; }
	}
}

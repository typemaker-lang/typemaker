using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IDecorated : ISyntaxNode
	{
		IReadOnlyList<IDecorator> Decorators { get; }
	}
}

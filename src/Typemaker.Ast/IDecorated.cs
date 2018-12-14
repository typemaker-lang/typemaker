using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IDecorated
	{
		IReadOnlyList<IDecorator> Decorators { get; }
	}
}

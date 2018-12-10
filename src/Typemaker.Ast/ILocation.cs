using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface ILocation
	{
		ulong Line { get; }
		ulong Column { get; }
	}
}

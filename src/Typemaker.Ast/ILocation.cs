using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Trivia
{
	public interface ILocation
	{
		ulong Line { get; }
		ulong Column { get; }
	}
}

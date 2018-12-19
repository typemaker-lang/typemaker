using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface ILocatable
	{
		Location Start { get; }
		Location End { get; }
	}
}

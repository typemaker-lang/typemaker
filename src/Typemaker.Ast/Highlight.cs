using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public class Highlight : ILocatable
	{
		public Location Start { get; set; }
		public Location End { get; set; }
	}
}

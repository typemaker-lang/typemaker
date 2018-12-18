using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public abstract class SyntaxNodeBase
	{
		public Location Start { get; set; }

		public Location End { get; set; }

		public bool Trivia { get; set; }
	}
}

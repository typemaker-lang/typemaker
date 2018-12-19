using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.ObjectTree
{
	public sealed class Location
	{
		public string FilePath { get; set; }

		public Ast.Location Start { get; set; }

		public Ast.Location End { get; set; }
	}
}

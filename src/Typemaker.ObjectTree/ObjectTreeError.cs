using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	public sealed class ObjectTreeError
	{
		public ObjectTreeErrorCode Code { get; set; }
		public string Description { get; set; }

		public Highlight Location { get; set; }
	}
}

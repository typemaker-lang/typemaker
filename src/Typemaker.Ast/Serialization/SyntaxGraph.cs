using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Serialization
{
	public sealed class SyntaxGraph : SyntaxNodeBase
	{
		public NodeType NodeType { get; set; }
		public Dictionary<string, object> Properties { get; set; }
		public List<SyntaxGraph> Children { get; set; }
	}
}

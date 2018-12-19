using System.Collections.Generic;
using Typemaker.Ast;

namespace Typemaker.Anaylsis
{
	public class SyntaxTransformation
	{
		public string Description { get; set; }
		public ISyntaxNode Target { get; set; }
		public List<ISyntaxNode> Transformation { get; set; }
	}
}
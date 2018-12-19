using System.Collections.Generic;
using Typemaker.Ast;

namespace Typemaker.Anaylsis
{
	public sealed class AnaylsisResult
	{
		public bool Error { get; set; }
		public ushort Code { get; set; }
		public bool Highlight { get; set; }
		public string Message { get; set; }
		public Location Start { get; set; }
		public Location End { get; set; }
		public List<SyntaxTransformation> Suggestions { get; set; }
	}
}
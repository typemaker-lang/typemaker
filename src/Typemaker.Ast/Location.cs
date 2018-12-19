using Antlr4.Runtime;

namespace Typemaker.Ast
{
	public class Location
	{
		public ulong Line { get; set; }

		public ulong Column { get; set; }
	}
}
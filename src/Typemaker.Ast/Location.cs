using Antlr4.Runtime;

namespace Typemaker.Ast
{
	public sealed class Location
	{
		public ulong Line { get; set; }

		public ulong Column { get; set; }
	}
}
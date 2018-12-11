using Antlr4.Runtime;
using Typemaker.Ast.Trivia;

namespace Typemaker.Ast
{
	sealed class Location : ILocation
	{
		public ulong Line { get; }

		public ulong Column { get; }

		public Location(IToken token, bool advanceOne)
		{
			Line = (ulong)token.Line;
			Column = (ulong)token.Column;
			if (advanceOne)
				++Column;
		}
	}
}
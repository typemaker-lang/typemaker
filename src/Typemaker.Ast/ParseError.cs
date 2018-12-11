using System;
using Typemaker.Ast.Trivia;

namespace Typemaker.Ast
{
	public sealed class ParseError : ILocation
	{
		public ulong Line { get; }

		public ulong Column { get; }

		string Description { get; }

		/// <summary>
		/// Translate specific parser error into human understandable sentences. i.e. Declaration ordering
		/// </summary>
		/// <param name="description">The original error description</param>
		/// <returns>The translated error description</returns>
		static string TranslateDescription(string description)
		{
			return description;
		}

		public ParseError(ulong line, ulong column, string description)
		{
			if (description == null)
				throw new ArgumentNullException(nameof(description));
			Line = line;
			Column = column;
			Description = TranslateDescription(description);
		}
	}
}
using System;

namespace Typemaker.Ast
{
	public sealed class ParseError
	{
		public string FilePath { get; }

		public Location Location { get; }

		public string Description { get; }

		public ParseError(string filePath, ulong line, ulong column, string description)
		{
			FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
			Description = description ?? throw new ArgumentNullException(nameof(description));
			Location = new Location
			{
				Line = line,
				Column = column
			};
		}
	}
}
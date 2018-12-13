using Antlr4.Runtime;
using System;
using System.Globalization;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	static class TokenFormatters
	{
		public static string ExtractResource(IToken resource)
		{
			if (resource == null)
				throw new ArgumentNullException(nameof(resource));
			if (resource.Type != TypemakerLexer.RES)
				throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Passed token type is {0}!", resource.Type));

			var text = resource.Text;
			return text.Substring(1, text.Length - 2);
		}
	}
}

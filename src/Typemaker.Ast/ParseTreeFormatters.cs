using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Globalization;
using System.Linq;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	static class ParseTreeFormatters
	{
		static void CheckNodeType(ITerminalNode node, int expectedType)
		{
			if (node.Symbol.Type != expectedType)
				throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Passed token type is {0}!", node.Symbol.Type));
		}

		public static string ExtractIdentifier(ITerminalNode identifier)
		{
			if (identifier == null)
				throw new ArgumentNullException(nameof(identifier));
			CheckNodeType(identifier, TypemakerLexer.IDENTIFIER);

			return identifier.Symbol.Text;
		}

		public static string ExtractResource(ITerminalNode resource)
		{
			if (resource == null)
				throw new ArgumentNullException(nameof(resource));
			CheckNodeType(resource, TypemakerLexer.RES);

			var text = resource.Symbol.Text;
			return text.Substring(1, text.Length - 2);
		}
	}
}

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

		public static string ExtractConstString(TypemakerParser.Const_stringContext context, out bool verbatim, out bool multiLine)
		{
			if (context == null)
				throw new InvalidOperationException(nameof(context));

			var multiLineVerbatim = context.MULTILINE_VERBATIUM_STRING();
			verbatim = (multiLineVerbatim ?? context.VERBATIUM_STRING()) != null;
			multiLine = (verbatim ? multiLineVerbatim : context.MULTI_STRING_START()) == null;

			return String.Concat(context.string_content().Select(x =>
			{
				var termNode = x.CHAR_INSIDE() ?? x.STRING_INSIDE() ?? x.MULTI_STRING_INSIDE() ?? x.MULTI_STRING_QUOTE();
				return termNode.Symbol.Text;
			}));
		}
	}
}

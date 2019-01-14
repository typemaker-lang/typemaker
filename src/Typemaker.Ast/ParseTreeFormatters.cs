using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	static class ParseTreeFormatters
	{
		static int CheckNodeType(ITerminalNode node, params int[] expectedTypes)
		{
			var nodeType = node.Symbol.Type;
			if (!expectedTypes.Any(x => x == nodeType))
				throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Passed token type is {0}!", node.Symbol.Type));
			return nodeType;
		}

		public static string ExtractIdentifier(ITerminalNode identifier)
		{
			if (identifier == null)
				throw new ArgumentNullException(nameof(identifier));
			CheckNodeType(identifier, TypemakerLexer.IDENTIFIER);

			return identifier.Symbol.Text;
		}

		public static string ExtractStringFormatter(TypemakerParser.String_bodyContext[] context)
		{
			Debug.Assert(context != null);  //remove this once you check the empty string doesn't null out
			
			if (context.Length == 0)
				return String.Empty;

			var builder = new StringBuilder();
			var embedCount = 0;

			foreach(var I in context)
			{
				var embed = I.expression();
				if (embed != null)
				{
					builder.Append('{');
					builder.Append(embedCount++);
					builder.Append('}');
				}
				else
					builder.Append(I.string_content().GetText());
			}

			return builder.ToString();
		}

		public static string ExtractVerbatimString(ITerminalNode verbatimString)
		{
			if (verbatimString == null)
				throw new ArgumentNullException(nameof(verbatimString));
			var nodeType = CheckNodeType(verbatimString, TypemakerLexer.MULTILINE_VERBATIUM_STRING, TypemakerLexer.VERBATIUM_STRING);

			var text = verbatimString.Symbol.Text;

			if (nodeType == TypemakerLexer.MULTILINE_VERBATIUM_STRING)
				return text.Substring(3, text.Length - 5);
			else
				return text.Substring(2, text.Length - 3);
		}

		public static string ExtractObjectPath(TypemakerParser.Extended_identifierContext extendedIdentifier, bool includeLast, out ObjectPath baseType)
		{
			if (extendedIdentifier == null)
				throw new ArgumentNullException(nameof(extendedIdentifier));

			string ExtractExtendedIdentifier() => ExtractIdentifier(extendedIdentifier.IDENTIFIER());
			IEnumerable<string> BuildExtendedDeclaration()
			{
				for (var fullyExtendedIdentifier = extendedIdentifier.fully_extended_identifier(); fullyExtendedIdentifier != null; fullyExtendedIdentifier = extendedIdentifier.fully_extended_identifier())
				{
					yield return ExtractExtendedIdentifier();
					extendedIdentifier = fullyExtendedIdentifier.extended_identifier();
				}
				if (includeLast)
					yield return ExtractExtendedIdentifier();
			}

			var parts = BuildExtendedDeclaration().ToList();
			baseType = parts.Count > 0 ? new ObjectPath(parts) : null;
			return includeLast ? null : ExtractExtendedIdentifier();
		}

		public static string ExtractObjectPath(TypemakerParser.Fully_extended_identifierContext fullyExtendedIdentifier, bool includeLast, out ObjectPath baseType) => ExtractObjectPath(fullyExtendedIdentifier.extended_identifier(), includeLast, out baseType);

		public static long? ExtractInteger(ITerminalNode integer)
		{
			if (integer == null)
				throw new ArgumentNullException(nameof(integer));
			CheckNodeType(integer, TypemakerLexer.INTEGER);

			if (!Int64.TryParse(integer.Symbol.Text, out var result))
				return null;
			return result;
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

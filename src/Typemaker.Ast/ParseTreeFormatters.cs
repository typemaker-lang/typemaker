using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	static class ParseTreeFormatters
	{
		static void CheckNodeType(ITerminalNode node, params int[] expectedTypes)
		{
			if (!expectedTypes.Any(x => x == node.Symbol.Type))
				throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Passed token type is {0}!", node.Symbol.Type));
		}

		public static string ExtractIdentifier(ITerminalNode identifier)
		{
			if (identifier == null)
				throw new ArgumentNullException(nameof(identifier));
			CheckNodeType(identifier, TypemakerLexer.IDENTIFIER);

			return identifier.Symbol.Text;
		}

		public static string ExtractObjectPath(TypemakerParser.Extended_identifierContext extendedIdentifier, out ObjectPath baseType)
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
			}

			var parts = BuildExtendedDeclaration().ToList();
			baseType = parts.Count > 0 ? new ObjectPath(parts) : null;
			return ExtractExtendedIdentifier();
		}

		public static string ExtractObjectPath(TypemakerParser.Fully_extended_identifierContext fullyExtendedIdentifier, out ObjectPath baseType) => ExtractObjectPath(fullyExtendedIdentifier.extended_identifier(), out baseType);

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

using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	interface ICompilationUnitContextFactory
	{
		TypemakerParser.Compilation_unitContext CreateCompilationUnitContext(Stream input, out Func<IList<IToken>> tokensAccessor, out IReadOnlyList<ParseError> parseErrors);
	}
}
using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	interface ICompilationUnitContextFactory
	{
		TypemakerParser.Compilation_unitContext CreateCompilationUnitContext(Stream input, out IReadOnlyList<ParseError> parseErrors);
	}
}
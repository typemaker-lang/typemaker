using System;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	sealed class CompilationUnitVisitor : TypemakerVisitor, ISyntaxTreeVisitor
	{
		readonly string filePath;

		public CompilationUnitVisitor(string filePath)
		{
			this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
		}

		public SyntaxTree Visit(TypemakerParser.Compilation_unitContext context) => throw new NotImplementedException();
	}
}

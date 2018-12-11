using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime.Misc;
using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	sealed class CompilationUnitVisitor : TypemakerParserBaseVisitor<SyntaxTree>
	{
		readonly string filePath;
		public CompilationUnitVisitor(string filePath)
		{
			this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
		}

		public override SyntaxTree VisitCompilation_unit([NotNull] TypemakerParser.Compilation_unitContext context)
		{
			var tree = new SyntaxTree(filePath, context);

			
			return tree;
		}
	}
}

using System;
using Antlr4.Runtime.Misc;
using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	sealed class CompilationUnitVisitor : TypemakerVisitor<SyntaxTree>
	{
		readonly string filePath;
		
		public CompilationUnitVisitor(string filePath)
		{
			this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
		}

		public override SyntaxTree VisitCompilation_unit([NotNull] TypemakerParser.Compilation_unitContext context) => new SyntaxTree(filePath, context, ConcatNodes(
			VisitWith<MapDeclarationVisitor, TypemakerParser.MapContext>(context.map())
			));
	}
}

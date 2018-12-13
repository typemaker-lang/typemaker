using System;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	sealed class CompilationUnitVisitor : TypemakerVisitor, ISyntaxTreeVisitor
	{
		readonly string filePath;

		readonly GlobalVarVisitor globalVarVisitor;
		readonly GlobalProcVisitor globalProcVisitor;
		readonly DatumVisitor datumVisitor;
		readonly DatumProcVisitor datumProcVisitor;
		readonly ExpressionVisitor expressionVisitor;

		public CompilationUnitVisitor(string filePath)
		{
			this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));

			globalVarVisitor = new GlobalVarVisitor();
			globalProcVisitor = new GlobalProcVisitor();
			datumVisitor = new DatumVisitor();
			datumProcVisitor = new DatumProcVisitor();
			expressionVisitor = new ExpressionVisitor();
		}

		public SyntaxTree Visit(TypemakerParser.Compilation_unitContext context) => new SyntaxTree(filePath, context, ConcatNodes(
			Visit(context.map()),
			VisitWith(context.global_var_declaration(), globalVarVisitor),
			VisitWith(context.var_definition_statement(), globalVarVisitor),
			Visit(context.generic_declaration()),
			VisitWith(context.global_proc_declaration(), globalProcVisitor),
			VisitWith(context.global_proc(), globalProcVisitor),
			VisitWith(context.datum_declaration(), datumVisitor),
			VisitWith(context.datum_def(), datumVisitor),
			VisitWith(context.datum_proc(), datumProcVisitor)
			));

		public override SyntaxNode VisitMap([NotNull] TypemakerParser.MapContext context) => new MapDeclaration(context);

		public override SyntaxNode VisitEnum([NotNull] TypemakerParser.EnumContext context) => new EnumDefinition(context, VisitEnumDefinitions(context.enum_block().enum_definitions()));

		IEnumerable<SyntaxNode> VisitEnumDefinitions(TypemakerParser.Enum_definitionsContext context)
		{
			yield return Visit(context.enum_definition());
			if (context.ChildCount > 1)
				foreach (var I in VisitEnumDefinitions(context.enum_definitions()))
					yield return I;
		}

		public override SyntaxNode VisitEnum_definition([NotNull] TypemakerParser.Enum_definitionContext context)
		{
			var value = context.enum_value();
			if (value == null)
				return new EnumItem(context, null);
			return new EnumItem(context, expressionVisitor.Visit(value));
		}

		public override SyntaxNode VisitInterface([NotNull] TypemakerParser.InterfaceContext context) => new Interface(context, Visit(context.interface_block().interface_definition()));

		public override SyntaxNode VisitInterface_definition([NotNull] TypemakerParser.Interface_definitionContext context)
		{
			return base.VisitInterface_definition(context);
		}
	}
}

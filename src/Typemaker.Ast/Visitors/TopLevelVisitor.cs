using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;
using Typemaker.Ast.Statements;
using Typemaker.Ast.Statements.Expressions;
using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	sealed class TopLevelVisitor : TypemakerVisitor, ISyntaxTreeVisitor
	{
		readonly string filePath;
		

		public TopLevelVisitor(string filePath)
		{
			this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
		}

		public SyntaxTree ConstructSyntaxTree(TypemakerParser.Compilation_unitContext context) => new SyntaxTree(filePath, context, ConcatNodes(Visit(context.top_level_declaration())));

		public override SyntaxNode VisitMap([NotNull] TypemakerParser.MapContext context) => new MapDeclaration(context);

		public override SyntaxNode VisitNullable_type([NotNull] TypemakerParser.Nullable_typeContext context)
		{
			//TODO: Child list and dict types
			return new NullableType(context, Enumerable.Empty<SyntaxNode>());
		}

		public override SyntaxNode VisitVar_declaration([NotNull] TypemakerParser.Var_declarationContext context)
		{

			var definitionStatement = context.var_definition_statement();
			var initializer = definitionStatement.expression();

			IEnumerable<SyntaxNode> children;
			if (initializer != null)
				children = ConcatNodes(
					Visit(context.decorator()),
					new List<SyntaxNode> { Visit(definitionStatement.var_definition_only().typed_identifier()) }
				);
			else
				children = ConcatNodes(
					Visit(context.decorator()),
					new List<SyntaxNode> {
						Visit(definitionStatement.var_definition_only().typed_identifier()),
						Visit(initializer)
					}
				);

			return new VarDeclaration(context, children);
		}

		public override SyntaxNode VisitEnum([NotNull] TypemakerParser.EnumContext context)
		{
			IEnumerable<SyntaxNode> VisitEnumItems()
			{
				for (var items = context.enum_block().enum_items(); items != null; items = items.enum_items())
					yield return Visit(items.enum_item());
			}

			return new EnumDefinition(context, VisitEnumItems());
		}

		public override SyntaxNode VisitEnum_item([NotNull] TypemakerParser.Enum_itemContext context)
		{
			var initializer = context.semicolonless_identifier_assignment();
			return new EnumItem(context, initializer != null ? Visit(initializer) : null);
		}

		public override SyntaxNode VisitInterface([NotNull] TypemakerParser.InterfaceContext context) => new Interface(context, ConcatNodes(Visit(context.declaration_block().declaration())));

		public override SyntaxNode VisitProc_definition([NotNull] TypemakerParser.Proc_definitionContext context) => new ProcDefinition(context, new List<SyntaxNode> { Visit(context.block()) });



		public override SyntaxNode VisitBlock([NotNull] TypemakerParser.BlockContext context) => new Block(context, Visit(context.statement()));

		public override SyntaxNode VisitVar_definition_statement([NotNull] TypemakerParser.Var_definition_statementContext context)
		{
			var definition = context.var_definition_only();
			var initializer = context.expression();
			return new VarDefinition(definition, initializer != null ? new List<SyntaxNode> { Visit(context. Visit(initializer) } : Enumerable.Empty<SyntaxNode>());
		}

		public override SyntaxNode VisitString([NotNull] TypemakerParser.StringContext context)
		{
			IEnumerable<SyntaxNode> StringEmbeds()
			{
				var body = context.string_body();
				foreach(var I in body)
				{
					var expr = I.expression();
					if (expr != null)
						yield return Visit(expr);
				}
			}

			return new StringExpression(context, StringEmbeds());
		}
	}
}

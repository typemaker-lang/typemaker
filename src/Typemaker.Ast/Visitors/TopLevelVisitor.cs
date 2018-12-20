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

		IReadOnlyList<SyntaxNode> GetMapChildren(TypemakerParser.True_typeContext context, out MapDefinitionType? definitionType)
		{
			var results = new List<SyntaxNode>();
			var rootType = context.root_type();
			if (rootType != null)
			{
				var listType = rootType.list_type();
				if (listType != null)
				{
					var nullableType = listType.nullable_type();
					if (nullableType != null)
					{
						definitionType = MapDefinitionType.FullyDefined;
						results.Add(Visit(nullableType));
					}
					else
						definitionType = MapDefinitionType.Undefined;
				}
				else
				{
					var dictType = rootType.dict_type();
					if (dictType != null)
					{
						var nullableType = dictType.nullable_type();
						if (nullableType.Length == 0)
							definitionType = MapDefinitionType.Undefined;
						else if (nullableType.Length == 2)
							definitionType = MapDefinitionType.FullyDefined;
						else if (dictType.BSLASH() != null)
							definitionType = MapDefinitionType.ValueOnly;
						else
							definitionType = MapDefinitionType.IndexOnly;
						results.AddRange(Visit(nullableType));
					}
					else
						definitionType = null;
				}
			}
			else
				definitionType = null;
			return results;
		}

		public override SyntaxNode VisitNullable_type([NotNull] TypemakerParser.Nullable_typeContext context) => new NullableType(context, GetMapChildren(context.true_type(), out var definitionType), definitionType);

		public override SyntaxNode VisitTrue_type([NotNull] TypemakerParser.True_typeContext context) => new TrueType(context, GetMapChildren(context, out var definitionType), definitionType);
		
		public override SyntaxNode VisitVar_declaration([NotNull] TypemakerParser.Var_declarationContext context)
		{
			var definitionStatement = context.var_definition_statement();
			var initializer = definitionStatement.expression();

			SyntaxNode VisitTypedIdentifier() => Visit(definitionStatement.var_definition_only().typed_identifier());

			IEnumerable<SyntaxNode> children;
			if (initializer != null)
				children = ConcatNodes(
					Visit(context.decorator()),
					new List<SyntaxNode> { VisitTypedIdentifier() }
				);
			else
				children = ConcatNodes(
					Visit(context.decorator()),
					new List<SyntaxNode> {
						VisitTypedIdentifier(),
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


		public override SyntaxNode VisitUnsafe_block([NotNull] TypemakerParser.Unsafe_blockContext context) => new Block(context, Visit(context.block().statement()));

		public override SyntaxNode VisitBlock([NotNull] TypemakerParser.BlockContext context) => new Block(context, Visit(context.statement()));

		public override SyntaxNode VisitVar_definition_statement([NotNull] TypemakerParser.Var_definition_statementContext context)
		{
			var definition = context.var_definition_only();
			var initializer = context.expression();
			var children = initializer != null ? new List<SyntaxNode> { Visit(initializer) } : Enumerable.Empty<SyntaxNode>();
			return new VarDefinition(definition, children);
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

		public override SyntaxNode VisitAccessed_target([NotNull] TypemakerParser.Accessed_targetContext context)
		{
			var accessedTarget = context.accessed_target();
			if (accessedTarget == null)
				return new IdentifierExpression(context.basic_identifier());
			return new AccessExpression(context, new List<SyntaxNode> { Visit(accessedTarget) });
		}
	}
}

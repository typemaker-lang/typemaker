using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Typemaker.Ast.Statements;
using Typemaker.Ast.Statements.Expressions;
using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	sealed class TypemakerVisitor : TypemakerParserBaseVisitor<SyntaxNode>, ISyntaxTreeVisitor
	{
		static IEnumerable<SyntaxNode> ConcatNodes(params IEnumerable<SyntaxNode>[] nodes)
		{
			var tmp = nodes[0];
			for (var I = 1; I < nodes.Length; ++I)
				tmp = Enumerable.Concat(tmp, nodes[I]);
			return tmp;
		}
		readonly string filePath;

		public TypemakerVisitor(string filePath)
		{
			this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
		}

		IEnumerable<SyntaxNode> Visit<TContext>(TContext[] contexts)
			where TContext : ParserRuleContext => contexts.Select(context => Visit(context));

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

		IEnumerable<SyntaxNode> VisitProcChildren(TypemakerParser.ProcContext context)
		{
			foreach (var I in Visit(context.decorator()))
				yield return I;
			for (var I = context.proc_arguments().argument_declaration_list(); I != null; I = I.argument_declaration_list())
				yield return Visit(I.argument_declaration());
			var retType = context.proc_return_declaration()?.return_type();
			if (retType != null)
				yield return Visit(retType.nullable_type());
		}

		public SyntaxTree ConstructSyntaxTree(TypemakerParser.Compilation_unitContext context) => new SyntaxTree(filePath, context, ConcatNodes(Visit(context.top_level_declaration())));

		public override SyntaxNode VisitMap([NotNull] TypemakerParser.MapContext context) => new MapDeclaration(context);

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

		public override SyntaxNode VisitProc_definition([NotNull] TypemakerParser.Proc_definitionContext context) => new ProcDefinition(context, ConcatNodes(VisitProcChildren(context.proc()), new List<SyntaxNode> { Visit(context.block()) }));


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

		public override SyntaxNode VisitDecorator([NotNull] TypemakerParser.DecoratorContext context) => new Decorator(context);

		public override SyntaxNode VisitNew_expression([NotNull] TypemakerParser.New_expressionContext context)
		{
			var children = new List<SyntaxNode>();
			var newType = context.new_type();
			if (newType != null)
				children.Add(Visit(newType));
			var argumentList = context.argument_list();
			if (argumentList != null)
				children.Add(Visit(argumentList));
			return new NewExpression(context, children);
		}

		public override SyntaxNode VisitArgument([NotNull] TypemakerParser.ArgumentContext context)
		{
			var identifier = context.IDENTIFIER();
			var expression = context.expression();
			if(identifier != null)
				return new Argument(context, new List<SyntaxNode> { Visit(identifier), Visit(expression) });
			return new Argument(context, new List<SyntaxNode> { Visit(expression) });
		}

		public override SyntaxNode VisitArgument_declaration([NotNull] TypemakerParser.Argument_declarationContext context)
		{
			var identifier = context.typed_identifier();
			var initializer = context.expression();
			if (initializer != null)
				return new ArgumentDeclaration(context, new List<SyntaxNode> { Visit(identifier), Visit(initializer) });
			return new ArgumentDeclaration(context, new List<SyntaxNode> { Visit(identifier) });
		}

		public override SyntaxNode VisitProc_declaration([NotNull] TypemakerParser.Proc_declarationContext context) => new ProcDeclaration(context, VisitProcChildren(context.proc()));
	}
}

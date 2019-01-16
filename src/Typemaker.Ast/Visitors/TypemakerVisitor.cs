using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Typemaker.Ast.Statements;
using Typemaker.Ast.Statements.Expressions;
using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	/// <summary>
	/// Visitor to convert <see cref="Parser"/> <see cref="ParserRuleContext"/>s into <see cref="SyntaxNode"/>s
	/// </summary>
	sealed class TypemakerVisitor : TypemakerParserBaseVisitor<IEnumerable<SyntaxNode>>, ISyntaxTreeVisitor
	{
		static IEnumerable<ITrivia> GetAllContextTokens(ParserRuleContext context)
		{
			foreach (var I in context.children)
			{
				var asTerminal = (ITerminalNode)I;
				yield return new Trivia(asTerminal.Symbol);
			}
		}

		static IEnumerable<ITrivia> GetContextTokensOnly(ParserRuleContext context)
		{
			foreach (var I in context.children)
			{
				if (I is ITerminalNode asTerminal)
					yield return new Trivia(asTerminal.Symbol);
				else
					foreach (var J in GetContextTokensOnly((ParserRuleContext)I))
						yield return J;
			}
		}

		static ITrivia Trivialize(SyntaxNode syntaxNode) => new Trivia(syntaxNode);

		readonly string filePath;

		public TypemakerVisitor(string filePath)
		{
			this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
		}

		IEnumerable<ITrivia> Visit<TContext>(TContext[] contexts)
			where TContext : ParserRuleContext => contexts.SelectMany(context =>
			{
				var result = Visit(context);
				Debug.Assert(result != null);
				return result;
			}).Select(x => Trivialize(x));

		IEnumerable<ITrivia> SelectAndVisitContextTokens(ParserRuleContext context) => SelectAndVisitContextTokens(context.children);
		IEnumerable<ITrivia> SelectAndVisitContextTokens(IEnumerable<IParseTree> parseTree, params ParserRuleContext[] tokenizeOnly)
		{
			var tokenizeIndex = 0;

			void FixIndex()
			{
				for (; tokenizeOnly.Length > tokenizeIndex && tokenizeOnly[tokenizeIndex] == null; ++tokenizeIndex) ;
			}

			FixIndex();

			Debug.Assert(parseTree != null);

			foreach (var I in parseTree)
			{
				if (tokenizeOnly.Length > tokenizeIndex && I == tokenizeOnly[tokenizeIndex])
				{
					foreach (var J in GetContextTokensOnly((ParserRuleContext)I))
						yield return J;

					++tokenizeIndex;
					FixIndex();
				}
				else if (I is ITerminalNode asTerminal)
					yield return new Trivia(asTerminal.Symbol);
				else
				{
					if (I is TypemakerParser.String_bodyContext)
						Debugger.Break();
					var childVisit = Visit(I);

					Debug.Assert(childVisit != null, "Missing visitor implementation!");

					foreach (var J in childVisit)
						yield return Trivialize(J);
				}
			}
		}

		protected override IEnumerable<SyntaxNode> AggregateResult(IEnumerable<SyntaxNode> aggregate, IEnumerable<SyntaxNode> nextResult) => aggregate == null || nextResult == null ? aggregate ?? nextResult : Enumerable.Concat(aggregate, nextResult);

		public SyntaxTree ConstructSyntaxTree(TypemakerParser.Compilation_unitContext context) => new SyntaxTree(filePath, Visit(context.top_level_declaration()));

		public override IEnumerable<SyntaxNode> VisitArgument([NotNull] TypemakerParser.ArgumentContext context)
		{
			var identifer = context.IDENTIFIER();
			string name = null;
			if (identifer != null)
				name = ParseTreeFormatters.ExtractIdentifier(identifer);
			yield return new Argument(name, SelectAndVisitContextTokens(context));
		}

		public override IEnumerable<SyntaxNode> VisitDecorator([NotNull] TypemakerParser.DecoratorContext context)
		{
			var precedence = context.precedence_decorator();
			if (precedence != null)
			{
				yield return new Decorator(DecoratorType.Precedence, GetContextTokensOnly(context));
				yield break;
			}

			var protection = context.access_decorator();
			if (protection != null)
			{
				yield return new Decorator(protection.PUBLIC() != null, GetContextTokensOnly(context));
				yield break;
			}

			//rest are 1 token, so switch it
			var parseTreeChild = context.children.Where(x => x is ITerminalNode terminalNode && new Token(terminalNode.Symbol).Class == TokenClass.Grammar).First();
			var tokenType = ((ITerminalNode)parseTreeChild).Symbol.Type;
			DecoratorType decoratorType;
			switch (tokenType)
			{
				case TypemakerLexer.ABSTRACT:
					decoratorType = DecoratorType.Abstract;
					break;
				case TypemakerLexer.DECLARE:
					decoratorType = DecoratorType.Declare;
					break;
				case TypemakerLexer.SEALED:
					decoratorType = DecoratorType.Sealed;
					break;
				case TypemakerLexer.EXPLICIT:
					decoratorType = DecoratorType.Explicit;
					break;
				case TypemakerLexer.READONLY:
					decoratorType = DecoratorType.Readonly;
					break;
				case TypemakerLexer.PARTIAL:
					decoratorType = DecoratorType.Partial;
					break;
				case TypemakerLexer.INLINE:
					decoratorType = DecoratorType.Inline;
					break;
				case TypemakerLexer.VIRTUAL:
					decoratorType = DecoratorType.Virtual;
					break;
				case TypemakerLexer.FINAL:
					decoratorType = DecoratorType.Readonly;
					break;
				case TypemakerLexer.ASYNC:
					decoratorType = DecoratorType.Async;
					break;
				case TypemakerLexer.ENTRYPOINT:
					decoratorType = DecoratorType.Entrypoint;
					break;
				case TypemakerLexer.YIELD:
					decoratorType = DecoratorType.Yield;
					break;
				default:
					throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Decorator context child is of type {0}!", tokenType));
			}

			yield return new Decorator(decoratorType, GetAllContextTokens(context));
		}

		public override IEnumerable<SyntaxNode> VisitEnum([NotNull] TypemakerParser.EnumContext context)
		{
			var enumType = context.enum_type();
			var identifier = enumType.IDENTIFIER();

			yield return new EnumDefinition(ParseTreeFormatters.ExtractIdentifier(identifier), SelectAndVisitContextTokens(context.children, enumType));
		}

		public override IEnumerable<SyntaxNode> VisitEnum_item([NotNull] TypemakerParser.Enum_itemContext context)
		{
			var identifier = context.IDENTIFIER();
			var name = ParseTreeFormatters.ExtractIdentifier(identifier);
			var auto = context.expression() != null;
			yield return new EnumItem(name, auto, SelectAndVisitContextTokens(context));
		}

		public override IEnumerable<SyntaxNode> VisitProc_definition([NotNull] TypemakerParser.Proc_definitionContext context)
		{
			var definition = context.proc();
			var identOrConst = definition.identifier_or_constructor();
			var identifier = identOrConst.IDENTIFIER();
			var fEI = definition.fully_extended_identifier();
			var procType = definition.proc_type();
			var arguments = definition.proc_arguments();
			var adl = arguments.argument_declaration_list();
			var rDec = definition.proc_return_declaration();

			ObjectPath objectPath = null;
			if (fEI != null)
				ParseTreeFormatters.ExtractObjectPath(fEI, true, out objectPath);

			IEnumerable<IParseTree> ExplodeProcDefinition()
			{
				foreach (var I in context.children)
				{
					if (I == definition)
						foreach (var J in definition.children)
							yield return J;
					else
						yield return I;
				}
			}

			IEnumerable<ITrivia> GetProcChildren(bool flattenRdec)
			{
				var toFlatten = new List<ParserRuleContext> { fEI, procType, identOrConst };
				if (adl == null)
					toFlatten.Add(arguments);
				if (flattenRdec)
					toFlatten.Add(rDec);
				return SelectAndVisitContextTokens(ExplodeProcDefinition(), toFlatten.ToArray());
			}

			if (identifier == null)
				yield return new ProcDefinition(objectPath, GetProcChildren(true));
			else
			{
				var name = ParseTreeFormatters.ExtractIdentifier(identifier);
				var isVerb = procType?.VERB() != null;
				var isVoid = rDec?.VOID() != null;
				yield return new ProcDefinition(name, isVoid, isVerb, objectPath, GetProcChildren(isVoid));
			}
		}

		public override IEnumerable<SyntaxNode> VisitVar_definition_statement([NotNull] TypemakerParser.Var_definition_statementContext context)
		{
			var vdo = context.var_definition_only();
			var typedIdentifer = vdo.typed_identifier();
			var identifier = typedIdentifer.IDENTIFIER();
			var type = typedIdentifer.type();
			var nullableType = type?.nullable_type();

			var isConst = type?.CONST() != null;

			var name = ParseTreeFormatters.ExtractIdentifier(identifier);

			IEnumerable<IParseTree> FlattenVDS()
			{
				foreach (var I in context.children)
					if (I == vdo)
						foreach (var J in vdo.children)
							if (J == typedIdentifer)
								foreach (var K in typedIdentifer.children)
									if (K == nullableType)
										foreach (var L in nullableType.children)
											yield return L;
									else
										yield return K;
							else
								yield return J;
					else
						yield return I;
			}

			yield return new VarDefinition(name, isConst, SelectAndVisitContextTokens(FlattenVDS()));
		}

		public override IEnumerable<SyntaxNode> VisitNullable_type([NotNull] TypemakerParser.Nullable_typeContext context)
		{
			var isNull = context.NULLABLE() != null;
			var trueType = context.true_type();
			var rootTypeContext = trueType.root_type();

			var children = GetContextTokensOnly(context);

			//use an if chain here cause it's really complicated
			if (rootTypeContext == null)
			{
				ParseTreeFormatters.ExtractObjectPath(trueType.extended_identifier(), true, out var path);
				yield return new NullableType(path, RootType.Object, isNull, children);
				yield break;
			}

			var listType = rootTypeContext.list_type();
			if (listType != null)
			{
				var mapType = listType.nullable_type() != null ? MapDefinitionType.FullyDefined : MapDefinitionType.Undefined;
				yield return new NullableType(RootType.List, mapType, isNull, children);
				yield break;
			}

			var dictType = rootTypeContext.dict_type();
			if (dictType != null)
			{
				var nullableTypes = dictType.nullable_type();
				var mapType = nullableTypes.Length == 2 ? MapDefinitionType.FullyDefined :
								nullableTypes.Length == 0 ? MapDefinitionType.Undefined :
								dictType.BSLASH() != null ? MapDefinitionType.ValueOnly :
								MapDefinitionType.IndexOnly;
				yield return new NullableType(RootType.Dict, mapType, isNull, children);
				yield break;
			}

			var enumType = rootTypeContext.enum_type();
			if (enumType != null)
			{
				var objectPath = new ObjectPath(new List<string> { ParseTreeFormatters.ExtractIdentifier(enumType.IDENTIFIER()) });
				yield return new NullableType(objectPath, RootType.Enum, isNull, children);
				yield break;
			}

			var interfaceType = rootTypeContext.interface_type();
			if (interfaceType != null)
			{
				var objectPath = new ObjectPath(new List<string> { ParseTreeFormatters.ExtractIdentifier(interfaceType.IDENTIFIER()) });
				yield return new NullableType(objectPath, RootType.Interface, isNull, children);
				yield break;
			}

			var pathType = rootTypeContext.path_type();
			if (pathType != null)
			{
				var pathRootType = pathType.CONCRETE() != null ? RootType.ConcretePath : RootType.Path;
				yield return new NullableType(pathRootType, null, isNull, children);
				yield break;
			}

			//switch the token for the remaining ones

			//rest are 1 token, so switch it
			var parseTreeChild = rootTypeContext.children.Where(x => x is ITerminalNode terminalNode && new Token(terminalNode.Symbol).Class == TokenClass.Grammar).First();
			var tokenType = ((ITerminalNode)parseTreeChild).Symbol.Type;

			RootType rootType;
			switch (tokenType)
			{
				case TypemakerLexer.BOOL:
					rootType = RootType.Bool;
					break;
				case TypemakerLexer.RESOURCE:
					rootType = RootType.Resource;
					break;
				case TypemakerLexer.INT:
					rootType = RootType.Integer;
					break;
				case TypemakerLexer.FLOAT:
					rootType = RootType.Float;
					break;
				case TypemakerLexer.EXCEPTION:
					rootType = RootType.Exception;
					break;
				case TypemakerLexer.STRING:
					rootType = RootType.String;
					break;
				default:
					throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Decorator context child is of type {0}!", tokenType));
			}

			yield return new NullableType(rootType, null, isNull, children);
		}

		public override IEnumerable<SyntaxNode> VisitString([NotNull] TypemakerParser.StringContext context)
		{
			var body = context.string_body();
			var formatter = ParseTreeFormatters.ExtractStringFormatter(body);
			var verbatim = context.VERBATIUM_STRING() != null || context.MULTILINE_VERBATIUM_STRING() != null;

			IEnumerable<IParseTree> SelectStringParts()
			{
				var bodyIndex = 0;
				foreach (var I in context.children)
					if (body.Length > bodyIndex && I == body[bodyIndex])
					{
						var thing = body[bodyIndex];
						var content = (ParserRuleContext)thing.string_content() ?? thing;
						foreach (var J in content.children)
							yield return J;
						++bodyIndex;
					}
					else
						yield return I;
			}

			yield return new StringExpression(formatter, verbatim, SelectAndVisitContextTokens(SelectStringParts()));
		}

		public override IEnumerable<SyntaxNode> VisitMap([NotNull] TypemakerParser.MapContext context)
		{
			var res = ParseTreeFormatters.ExtractResource(context.RES());
			yield return new MapDeclaration(res, GetAllContextTokens(context));
		}

		public override IEnumerable<SyntaxNode> VisitBlock([NotNull] TypemakerParser.BlockContext context)
		{
			var isUnsafe = context.UNSAFE() != null;
			var blockInterior = context.block_interior();

			IEnumerable<IParseTree> FlattenBlock()
			{
				foreach (var I in context.children)
					if (I == blockInterior)
						foreach (var J in blockInterior.children)
							yield return J;
					else
						yield return I;
			}

			yield return new Block(isUnsafe, SelectAndVisitContextTokens(FlattenBlock()));
		}

		public override IEnumerable<SyntaxNode> VisitUnsafe_block([NotNull] TypemakerParser.Unsafe_blockContext context)
		{
			var blockInterior = context.block_interior();
			IEnumerable<IParseTree> FlattenBlock()
			{
				foreach (var I in context.children)
					if (I == blockInterior)
						foreach (var J in blockInterior.children)
							yield return J;
					else
						yield return I;
			}

			yield return new Block(true, SelectAndVisitContextTokens(FlattenBlock()));
		}
	}
}

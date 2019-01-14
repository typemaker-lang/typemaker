using System;
using System.Collections.Generic;
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
		static IEnumerable<ITrivia> ConcatNodes(params IEnumerable<ITrivia>[] nodes)
		{
			foreach (var I in nodes)
				foreach (var J in I)
					yield return J;
		}

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
			where TContext : ParserRuleContext => contexts.SelectMany(context => Visit(context)).Select(x => Trivialize(x));

		IEnumerable<ITrivia> SelectAndVisitContextTokens(ParserRuleContext context)
		{
			foreach (var I in context.children)
			{
				if (I is ITerminalNode asTerminal)
					yield return new Trivia(asTerminal.Symbol);
				else
					foreach (var J in Visit(I))
						yield return Trivialize(J);
			}
		}

		protected override IEnumerable<SyntaxNode> AggregateResult(IEnumerable<SyntaxNode> aggregate, IEnumerable<SyntaxNode> nextResult) => Enumerable.Concat(aggregate, nextResult);

		public SyntaxTree ConstructSyntaxTree(TypemakerParser.Compilation_unitContext context) => new SyntaxTree(filePath, ConcatNodes(Visit(context.top_level_declaration())));

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
				yield return new Decorator(DecoratorType.Precedence, SelectAndVisitContextTokens(context));
				yield break;
			}

			var protection = context.access_decorator();
			if (protection != null)
			{
				yield return new Decorator(protection.PUBLIC() != null, SelectAndVisitContextTokens(context));
				yield break;
			}

			//rest are 1 token, so switch it
			var parseTreeChild = context.children.Where(x => x is ITerminalNode terminalNode && new Token(terminalNode.Symbol).Class != TokenClass.Grammar).First();
			var tokenType = ((ITerminalNode)parseTreeChild)?.Symbol.Type;
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
				case null:
					throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "First parse tree child is of type {0}!", parseTreeChild.GetType()));
				default:
					throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Decorator context child is of type {0}!", tokenType));
			}

			yield return new Decorator(decoratorType, SelectAndVisitContextTokens(context));
		}
	}
}

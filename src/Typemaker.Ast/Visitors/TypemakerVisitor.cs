using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Typemaker.Ast.Statements;
using Typemaker.Ast.Statements.Expressions;
using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	/// <summary>
	/// Visitor to convert <see cref="Parser"/> <see cref="ParserRuleContext"/>s into <see cref="SyntaxNode"/>s
	/// </summary>
	sealed class TypemakerVisitor : TypemakerParserBaseVisitor<IInternalTrivia>, ISyntaxTreeVisitor
	{
		static IEnumerable<IInternalTrivia> ConcatNodes(params IEnumerable<IInternalTrivia>[] nodes)
		{
			foreach (var I in nodes)
				foreach (var J in I)
					yield return J;
		}

		static IEnumerable<IInternalTrivia> GetAllContextTokens(ParserRuleContext context)
		{
			foreach(var I in context.children)
			{
				var asTerminal = (ITerminalNode)I;
				yield return new InternalTrivia(asTerminal.Symbol);
			}
		}

		readonly string filePath;
		
		public TypemakerVisitor(string filePath)
		{
			this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
		}

		IEnumerable<IInternalTrivia> SelectAndVisitContextTokens(ParserRuleContext context)
		{
			foreach (var I in context.children)
			{
				if (I is ITerminalNode asTerminal)
					yield return new InternalTrivia(asTerminal.Symbol);
				else
					yield return Visit(I);
			}
		}

		public SyntaxTree ConstructSyntaxTree(TypemakerParser.Compilation_unitContext context) => new SyntaxTree(filePath, context, null /* TODO */);
	}
}

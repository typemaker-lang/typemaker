using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class Decorator : SyntaxNode, IDecorator
	{
		public DecoratorType Type { get; }

		public long? Precedence { get; }

		public bool? PublicProtection { get; }

		public Decorator(TypemakerParser.DecoratorContext context) : base(context, Enumerable.Empty<SyntaxNode>())
		{
			var precedence = context.precedence_decorator();
			if(precedence != null)
			{
				Type = DecoratorType.Precedence;
				var integer = precedence.integer();
				Precedence = ParseTreeFormatters.ExtractInteger(integer.INTEGER());
				if (integer.MINUS() != null)
					Precedence *= -1;
				return;
			}

			var protection = context.access_decorator();
			if(protection != null)
			{
				Type = DecoratorType.Protection;
				PublicProtection = protection.PUBLIC() != null;
				return;
			}

			//rest are 1 token, so switch it
			var parseTreeChild = context.children.First();
			var tokenType = (parseTreeChild as ITerminalNode)?.Symbol.Type;
			switch (tokenType)
			{
				case TypemakerLexer.ABSTRACT:
					Type = DecoratorType.Abstract;
					break;
				case TypemakerLexer.DECLARE:
					Type = DecoratorType.Declare;
					break;
				case TypemakerLexer.SEALED:
					Type = DecoratorType.Sealed;
					break;
				case TypemakerLexer.EXPLICIT:
					Type = DecoratorType.Explicit;
					break;
				case TypemakerLexer.READONLY:
					Type = DecoratorType.Readonly;
					break;
				case TypemakerLexer.PARTIAL:
					Type = DecoratorType.Partial;
					break;
				case TypemakerLexer.INLINE:
					Type = DecoratorType.Inline;
					break;
				case TypemakerLexer.VIRTUAL:
					Type = DecoratorType.Virtual;
					break;
				case TypemakerLexer.FINAL:
					Type = DecoratorType.Readonly;
					break;
				case null:
					throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "First parse tree child is of type {0}!", parseTreeChild.GetType()));
				default:
					throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Decorator context child is of type {0}!", tokenType));
			}
		}
	}
}

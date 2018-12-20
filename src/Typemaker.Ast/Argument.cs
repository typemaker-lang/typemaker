using System;
using System.Collections.Generic;
using Typemaker.Ast.Statements.Expressions;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class Argument : SyntaxNode, IArgument
	{
		public string Name { get; }
		public IExpression Value => ChildAs<IExpression>();

		public Argument(TypemakerParser.ArgumentContext context, IEnumerable<SyntaxNode> children) : base(context, children)
		{
			var identifier = context.IDENTIFIER();
			if (identifier != null)
				Name = ParseTreeFormatters.ExtractIdentifier(identifier);
		}
	}
}

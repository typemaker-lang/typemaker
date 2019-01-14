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

		public Argument(string name, IEnumerable<ITrivia> children) : base(children)
		{
			Name = name;
		}
	}
}

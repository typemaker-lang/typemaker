using System;
using System.Collections.Generic;
using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast
{
	sealed class IdentifierDeclaration : SyntaxNode, IIdentifierDeclaration
	{
		public INullableType Typepath => ChildAs<INullableType>();

		public IExpression Initializer => ChildAs<IExpression>();

		public string Name { get; }

		public IdentifierDeclaration(string name, IEnumerable<ITrivia> children): base(children)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}
	}
}

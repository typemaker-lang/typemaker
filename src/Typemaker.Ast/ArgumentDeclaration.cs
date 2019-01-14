using System.Collections.Generic;
using Typemaker.Ast.Statements.Expressions;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	/// <inheritdoc />
	sealed class ArgumentDeclaration : SyntaxNode, IArgumentDeclaration
	{
		/// <inheritdoc />
		public INullableType VariadicType => ChildAs<INullableType>();

		/// <inheritdoc />
		public IIdentifierDeclaration IdentifierDeclaration => ChildAs<IIdentifierDeclaration>();

		/// <inheritdoc />
		public IExpression Initializer => ChildAs<IExpression>();

		/// <summary>
		/// Construct an <see cref="ArgumentDeclaration"/>
		/// </summary>
		/// <param name="children">The child <see cref="ITrivia"/>s</param>
		public ArgumentDeclaration(IEnumerable<ITrivia> children) : base(children)
		{
		}
	}
}

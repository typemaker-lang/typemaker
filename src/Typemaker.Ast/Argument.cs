using System.Collections.Generic;
using Typemaker.Ast.Statements.Expressions;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	/// <inheritdoc />
	sealed class Argument : SyntaxNode, IArgument
	{
		/// <inheritdoc />
		public INullableType VariadicType => ChildAs<INullableType>();

		/// <inheritdoc />
		public IIdentifierDeclaration IdentifierDeclaration => ChildAs<IIdentifierDeclaration>();

		/// <inheritdoc />
		public IExpression Initializer => ChildAs<IExpression>();

		/// <summary>
		/// Construct an <see cref="Argument"/>
		/// </summary>
		/// <param name="context">The <see cref="TypemakerParser.Argument_declarationContext"/></param>
		/// <param name="children">The child <see cref="SyntaxNode"/>s</param>
		public Argument(TypemakerParser.Argument_declarationContext context, IEnumerable<SyntaxNode> children) : base(context, children)
		{
			AntiTriviaContext(context.typed_identifier());
		}
	}
}

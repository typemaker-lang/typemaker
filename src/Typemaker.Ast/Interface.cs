using System.Collections.Generic;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	/// <inheritdoc />
	sealed class Interface : SyntaxNode, IInterface
	{
		/// <inheritdoc />
		public string Name { get; }

		/// <inheritdoc />
		public IReadOnlyList<IVarDeclaration> VarDeclarations => ChildrenAs<IVarDeclaration>();

		/// <inheritdoc />
		public IReadOnlyList<IProcDeclaration> ProcDeclarations => ChildrenAs<IProcDeclaration>();

		/// <inheritdoc />
		public IReadOnlyList<IImplements> Implements => ChildrenAs<IImplements>();

		/// <summary>
		/// Construct an <see cref="Interface"/>
		/// </summary>
		/// <param name="context">The <see cref="TypemakerParser.InterfaceContext"/></param>
		/// <param name="children">The child <see cref="SyntaxNode"/>s</param>
		public Interface(TypemakerParser.InterfaceContext context, IEnumerable<SyntaxNode> children) : base(context, children)
		{
			Name = ParseTreeFormatters.ExtractIdentifier(context.interface_type().IDENTIFIER());
		}
	}
}

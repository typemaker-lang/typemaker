using System;
using System.Collections.Generic;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	abstract class GenericDeclaration : SyntaxNode, IGenericDeclaration
	{
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
		/// <param name="name">The value of <see cref="Name"/></param>
		/// <param name="context">The <see cref="TypemakerParser.InterfaceContext"/></param>
		/// <param name="children">The child <see cref="SyntaxNode"/>s</param>
		protected GenericDeclaration(string name, TypemakerParser.Declaration_blockContext context, IEnumerable<SyntaxNode> children) : base(context, children)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}
	}
}
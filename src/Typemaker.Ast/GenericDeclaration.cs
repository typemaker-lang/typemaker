using System;
using System.Collections.Generic;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	abstract class GenericDeclaration : SyntaxNode, IGenericDeclaration
	{
		public string Name { get; }

		/// <inheritdoc />
		public IEnumerable<IVarDeclaration> VarDeclarations => ChildrenAs<IVarDeclaration>();

		/// <inheritdoc />
		public IEnumerable<IProcDeclaration> ProcDeclarations => ChildrenAs<IProcDeclaration>();

		/// <inheritdoc />
		public IEnumerable<IImplements> Implements => ChildrenAs<IImplements>();

		/// <summary>
		/// Construct an <see cref="Interface"/>
		/// </summary>
		/// <param name="name">The value of <see cref="Name"/></param>
		/// <param name="context">The <see cref="TypemakerParser.InterfaceContext"/></param>
		/// <param name="children">The child <see cref="IInternalTrivia"/>s</param>
		protected GenericDeclaration(string name, TypemakerParser.Declaration_blockContext context, IEnumerable<IInternalTrivia> children) : base(context, children)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}
	}
}
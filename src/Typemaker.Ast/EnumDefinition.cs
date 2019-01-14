using System;
using System.Collections.Generic;

namespace Typemaker.Ast
{
	/// <inheritdoc />
	sealed class EnumDefinition : SyntaxNode, IEnumDefinition
	{
		/// <inheritdoc />
		public string Name { get; }

		/// <inheritdoc />
		public IEnumerable<IEnumItem> Items => ChildrenAs<IEnumItem>();

		/// <summary>
		/// Construct an <see cref="EnumDefinition"/>
		/// </summary>
		/// <param name="name">The value of <see cref="Name"/></param>
		/// <param name="children">The child <see cref="ITrivia"/>s</param>
		public EnumDefinition(string name, IEnumerable<ITrivia> children) : base(children)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}
	}
}

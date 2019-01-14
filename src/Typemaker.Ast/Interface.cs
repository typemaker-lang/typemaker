using System.Collections.Generic;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	/// <inheritdoc />
	sealed class Interface : GenericDeclaration, IInterface
	{
		/// <summary>
		/// Construct an <see cref="Interface"/>
		/// </summary>
		/// <param name="context">The <see cref="TypemakerParser.InterfaceContext"/></param>
		/// <param name="children">The child <see cref="IInternalTrivia"/>s</param>
		public Interface(TypemakerParser.InterfaceContext context, IEnumerable<IInternalTrivia> children) : base(ParseTreeFormatters.ExtractIdentifier(context.interface_type().IDENTIFIER()), context.declaration_block(), children)
		{
		}
	}
}

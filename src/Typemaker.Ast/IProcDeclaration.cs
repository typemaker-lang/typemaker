using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IProcDeclaration : IIdentifiable, IDecorated
	{
		bool IsVerb { get; }

		bool IsConstructor { get; }

		IReadOnlyList<IIdentifierDeclaration> Arguments { get; }

		INullableType ReturnType { get; }

		IProcDefinition Definition { get; }
	}
}

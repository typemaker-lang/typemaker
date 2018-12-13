using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IProcDeclaration : IIdentifiable
	{
		bool IsVerb { get; }

		bool IsConstructor { get; }

		IReadOnlyList<ITypedIdentifier> Arguments { get; }

		INullableType ReturnType { get; }

		IProcDefinition Definition { get; }
	}
}

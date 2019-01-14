using System.Collections.Generic;

namespace Typemaker.Ast
{
	public interface IProcDeclaration : IIdentifiable, IDecorated
	{
		bool IsVerb { get; }

		bool IsConstructor { get; }

		IObjectPath ObjectPath { get; }

		INullableType ReturnType { get; }

		IEnumerable<IArgumentDeclaration> Arguments { get; }
	}
}

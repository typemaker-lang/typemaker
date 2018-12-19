using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IProcDeclaration : ILocatable, IIdentifiable
	{
		bool IsVerb { get; }

		ITypeDeclaration ReturnType { get; }

		IProcDefinition Definition { get; }

		IReadOnlyList<IArgumentDeclaration> Arguments { get; }
	}
}

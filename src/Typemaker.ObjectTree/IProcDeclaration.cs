using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IProcDeclaration : IIdentifiable
	{
		bool IsVerb { get; }

		ITypeDeclaration ReturnType { get; }

		IReadOnlyList<IArgumentDeclaration> Arguments { get; }
	}
}

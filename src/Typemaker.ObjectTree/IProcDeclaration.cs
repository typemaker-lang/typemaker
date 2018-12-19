using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IProcDeclaration : ILocatable, INameable, IInlineable
	{
		bool IsVerb { get; }

		IReturnType ReturnType { get; }

		IReadOnlyList<IArgumentDeclaration> Arguments { get; }
	}
}

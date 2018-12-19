using System.Collections.Generic;
using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	public interface IProcDeclaration : ILocatable, IIdentifiable
	{
		bool IsVerb { get; }

		ITypeDeclaration ReturnType { get; }

		IReadOnlyList<IArgumentDeclaration> Arguments { get; }
	}
}

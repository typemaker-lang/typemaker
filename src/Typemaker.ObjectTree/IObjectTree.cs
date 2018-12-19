using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IObjectTree
	{
		IObject RootObject { get; }

		IReadOnlyList<IGlobalVariableDeclaration> Variables { get; }

		IReadOnlyList<IProcDeclaration> Procs { get; }

		IReadOnlyList<IEnum> Enums { get; }

		IReadOnlyList<IInterface> Interfaces { get; }

		IObject LookupPath(string extendedIdentifier);
	}
}

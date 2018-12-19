using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IObjectTree
	{
		IObject RootObject { get; }

		IReadOnlyList<IVariableDeclaration> Variables { get; }

		IReadOnlyList<IProcDeclaration> DeclaredProcs { get; }

		IReadOnlyList<IProcDefinition> DefinedProcs { get; }

		IReadOnlyList<IEnum> Enums { get; }

		IReadOnlyList<IInterface> Interfaces { get; }
	}
}

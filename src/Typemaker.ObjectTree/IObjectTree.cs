using System.Collections.Generic;
using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	public interface IObjectTree : IRemovableChildren
	{
		IObject RootObject { get; }

		IEnumerable<IObject> RootedObjects { get; }

		IReadOnlyList<IVariableDeclaration> Variables { get; }

		IReadOnlyList<IProcDefinition> Procs { get; }

		IReadOnlyList<IEnumDeclaration> Enums { get; }

		IReadOnlyList<IInterface> Interfaces { get; }

		IObject LookupPath(ObjectPath path);
	}
}

using System.Collections.Generic;
using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	public interface IObjectTree : IRemovableChildren, IValidatable
	{
		IObject RootObject { get; }

		IEnumerable<IObject> RootedObjects { get; }

		IReadOnlyList<IVariableDeclaration> Variables { get; }

		IReadOnlyList<IProcDefinition> Procs { get; }

		IReadOnlyList<IEnumDeclaration> Enums { get; }

		IReadOnlyList<IInterface> Interfaces { get; }

		IObject LookupPath(ObjectPath path);

		void AddVariable(IVariableDeclaration variable);
		void AddProc(IProcDefinition proc);
		void AddEnum(IEnumDeclaration enumDeclaration);
		void AddInterface(IInterface interfaceDefinition);
	}
}

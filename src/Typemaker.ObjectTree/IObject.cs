using System.Collections.Generic;

namespace Typemaker.CodeTree
{
	public interface IObject : IDeclarable, INameable
	{
		bool IsSealed { get; }

		bool IsRooted { get; }

		bool IsAbstract { get; }

		bool IsPartial { get; }

		IObject ParentType { get; }

		IConstructor Constructor { get; }

		IReadOnlyList<IObject> Subtypes { get; }

		IReadOnlyList<IObjectVariableDeclaration> Variables { get; }

		IReadOnlyList<IObjectProcDeclaration> DeclaredProcs { get; }

		IReadOnlyList<IObjectProcDefinition> DefinedProcs { get; }
	}
}

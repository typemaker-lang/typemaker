using System.Collections.Generic;
using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	public interface IObject : IDeclarable, IIdentifiable, IImplementer
	{
		bool IsSealed { get; }

		/// <summary>
		/// If the <see cref="IObject"/>'s path is in the form `/&lt;name&gt;
		/// </summary>
		bool IsRooted { get; }

		bool IsAbstract { get; }

		bool IsPartial { get; }

		IObject ParentType { get; }

		IConstructor Constructor { get; }

		IReadOnlyList<ILocatable> DeclarationLocations { get; }

		IReadOnlyList<IObject> Subtypes { get; }

		IReadOnlyList<IObjectVariableDeclaration> Variables { get; }

		IReadOnlyList<IObjectProcDeclaration> DeclaredProcs { get; }

		IReadOnlyList<IObjectProcDefinition> DefinedProcs { get; }
	}
}

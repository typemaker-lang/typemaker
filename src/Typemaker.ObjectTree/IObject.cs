using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IObject : IObjectDeclarationHolder
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
	}
}

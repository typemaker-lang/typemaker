using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IObject : IObjectDeclarationHolder, IRemovableChildren
	{
		string FullyExtendedName { get; }
		string FullyQualifiedName { get; }

		ObjectVirtuality Virtuality { get; }

		/// <summary>
		/// If the <see cref="IObject"/>'s path is in the form `/&lt;name&gt;
		/// </summary>
		bool IsRooted { get; }

		bool IsPartial { get; }

		IObject ParentType { get; }

		IConstructor Constructor { get; }

		IReadOnlyList<ILocatable> Locations { get; }

		IReadOnlyList<IObject> Subtypes { get; }

		void AddLocation(ILocatable location);
		void AddSubtype(IObject subtype);
		void AddConstructor(IConstructor constructor);
	}
}

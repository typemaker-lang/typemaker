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
		bool SelfMath { get; }
		bool AutoconvertResource { get; }

		IObject ParentType { get; }

		IReadOnlyList<Highlight> Locations { get; }

		IReadOnlyList<IObject> Subtypes { get; }

		IReadOnlyList<IObjectProcDefinition> Procs { get; }

		void AddLocation(Highlight location);
		void AddSubtype(IObject subtype);
	}
}

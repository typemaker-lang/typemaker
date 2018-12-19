using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IEnumDeclaration : IIdentifiable, ILocatable, IRemovableChildren
	{
		IReadOnlyList<IEnumObject> Values { get; }
	}
}

using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IEnumDeclaration : IIdentifiable, ILocatable, IRemovableChildren, IValidatable
	{
		IReadOnlyList<IEnumObject> Values { get; }
	}
}

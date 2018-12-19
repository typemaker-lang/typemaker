using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IEnumDeclaration : IIdentifiable, ILocatable, IValidatable
	{
		IReadOnlyList<IEnumItem> Values { get; }
	}
}

using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IEnum : IIdentifiable, ILocatable
	{
		IReadOnlyList<IEnumObject> Values { get; }
	}
}

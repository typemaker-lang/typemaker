using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IEnum : INameable, ILocatable
	{
		IReadOnlyList<IEnumObject> Values { get; }
	}
}

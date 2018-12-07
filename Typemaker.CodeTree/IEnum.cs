using System.Collections.Generic;

namespace Typemaker.CodeTree
{
	public interface IEnum : INameable, ILocatable
	{
		IReadOnlyList<IEnumObject> Values { get; }
	}
}

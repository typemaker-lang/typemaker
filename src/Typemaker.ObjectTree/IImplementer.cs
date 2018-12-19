using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IImplementer
	{
		IReadOnlyList<IInterface> Implements { get; }
	}
}
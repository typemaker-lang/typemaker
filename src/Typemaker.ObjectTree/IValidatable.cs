using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IValidatable
	{
		IEnumerable<ObjectTreeError> Validate();
	}
}
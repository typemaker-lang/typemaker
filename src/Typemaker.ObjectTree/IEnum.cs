using System.Collections.Generic;
using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	public interface IEnum : IIdentifiable, ILocatable
	{
		IReadOnlyList<IEnumObject> Values { get; }
	}
}

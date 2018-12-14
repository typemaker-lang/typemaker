using System.Collections.Generic;

namespace Typemaker.Ast
{
	public interface IEnumDefinition : IIdentifiable
	{
		IReadOnlyList<IEnumItem> Items { get; }
	}
}

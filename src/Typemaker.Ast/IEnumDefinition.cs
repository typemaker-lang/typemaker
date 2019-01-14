using System.Collections.Generic;

namespace Typemaker.Ast
{
	public interface IEnumDefinition : IIdentifiable
	{
		IEnumerable<IEnumItem> Items { get; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IEnumDefinition : IGenericDeclaration
	{
		IReadOnlyList<IEnumItem> Items { get; }
	}
}

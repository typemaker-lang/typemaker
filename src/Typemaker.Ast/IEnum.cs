using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IEnum : IGenericDeclaration
	{
		IReadOnlyList<IEnumItem> Items { get; }
	}
}

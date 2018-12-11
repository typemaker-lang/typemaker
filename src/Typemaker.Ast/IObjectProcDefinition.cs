using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IObjectProcDefinition : IProcDefinition
	{
		new IObjectDeclaration Declaration { get; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IObjectProcDefinition : IProcDefinition
	{
		bool IsFinal { get; }

		int Precedence { get; }

		new IObjectProcDeclaration Declaration { get; }
	}
}

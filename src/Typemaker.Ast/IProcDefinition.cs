using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IProcDefinition : ISyntaxNode
	{
		bool IsInline { get; }

		IProcDeclaration Declaration { get; }

		IBlock Body { get; }
	}
}

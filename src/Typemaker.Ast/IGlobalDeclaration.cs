using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IGlobalDeclaration : ISyntaxNode
	{
		bool IsDeclared { get; }
	}
}

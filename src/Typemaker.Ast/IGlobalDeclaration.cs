using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	interface IGlobalDeclaration
	{
		bool IsDeclared { get; }
	}
}

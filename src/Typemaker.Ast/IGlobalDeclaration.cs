using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IGlobalDeclaration : IIdentifiable
	{
		bool IsDeclared { get; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IVarDeclaration : ISyntaxNode, IIdentifiable
	{
		bool IsConst { get; }
	}
}

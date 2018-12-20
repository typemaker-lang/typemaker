using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Validation
{
	public interface IValidSyntaxTree
	{
		ISyntaxTree SyntaxTree { get; }
	}
}

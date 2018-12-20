using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Validation
{
	public interface ISyntaxTreeValidator
	{
		SyntaxTreeValidationResult ValidateSyntaxTree(ISyntaxTree syntaxTree);
	}
}

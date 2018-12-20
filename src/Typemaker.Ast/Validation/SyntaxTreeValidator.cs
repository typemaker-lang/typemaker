using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Validation
{
	public sealed class SyntaxTreeValidator : ISyntaxTreeValidator
	{
		public SyntaxTreeValidationResult ValidateSyntaxTree(ISyntaxTree syntaxTree)
		{
			if (syntaxTree == null)
				throw new ArgumentNullException(nameof(syntaxTree));

			throw new NotImplementedException();
		}
	}
}

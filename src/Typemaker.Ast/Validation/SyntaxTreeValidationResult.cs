using System.Collections.Generic;

namespace Typemaker.Ast.Validation
{
	public sealed class SyntaxTreeValidationResult
	{
		public IValidSyntaxTree SyntaxTree { get; set; }

		public IReadOnlyList<ValidationError> Errors { get; set; }
	}
}
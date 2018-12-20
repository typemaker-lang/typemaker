using System;

namespace Typemaker.Ast.Validation
{
	sealed class ValidSyntaxTree : IValidSyntaxTree
	{
		public ISyntaxTree SyntaxTree { get; }

		public ValidSyntaxTree(ISyntaxTree syntaxTree)
		{
			SyntaxTree = syntaxTree ?? throw new ArgumentNullException(nameof(syntaxTree));
		}
	}
}

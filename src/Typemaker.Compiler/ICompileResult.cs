using System.Collections.Generic;
using Typemaker.Ast.Validation;

namespace Typemaker.Compiler
{
	public interface ICompileResult
	{
		IReadOnlyList<IValidSyntaxTree> ValidSyntaxTrees { get; }

		IReadOnlyList<CompilerError> Errors { get; }
	}
}
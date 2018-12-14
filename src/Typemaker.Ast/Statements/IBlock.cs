using System.Collections.Generic;

namespace Typemaker.Ast.Statements
{
	public interface IBlock : IStatement
	{
		bool Unsafe { get; }

		IReadOnlyList<IStatement> Statements { get; }
	}
}

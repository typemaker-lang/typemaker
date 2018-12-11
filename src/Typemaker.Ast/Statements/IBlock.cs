using System.Collections.Generic;

namespace Typemaker.Ast.Statements
{
	public interface IBlock : IStatement
	{
		IReadOnlyList<ISetStatement> SetStatements { get; }
		IReadOnlyList<IStatement> RegularStatements { get; }
	}
}

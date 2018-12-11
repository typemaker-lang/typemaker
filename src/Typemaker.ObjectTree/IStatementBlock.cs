using System.Collections.Generic;

namespace Typemaker.CodeTree
{
	public interface IStatementBlock
	{
		IReadOnlyList<IStatement> Statements { get; }
	}
}
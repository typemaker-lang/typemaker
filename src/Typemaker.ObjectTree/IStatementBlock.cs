using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IStatementBlock
	{
		IReadOnlyList<IStatement> Statements { get; }
	}
}
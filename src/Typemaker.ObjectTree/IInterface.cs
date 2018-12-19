using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IInterface: INameable, ILocatable
	{
		IInterface Parent { get; }

		IReadOnlyList<IVariableDeclaration> VariableDeclarations { get; }

		IReadOnlyList<IObjectProcDeclaration> ProcDeclarations { get; }
	}
}

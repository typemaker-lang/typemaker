using System.Collections.Generic;

namespace Typemaker.CodeTree
{
	public interface IInterface: INameable, ILocatable
	{
		IInterface Parent { get; }

		IReadOnlyList<IVariableDeclaration> VariableDeclarations { get; }

		IReadOnlyList<IObjectProcDeclaration> ProcDeclarations { get; }
	}
}

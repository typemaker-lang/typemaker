using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IInterface: IIdentifiable, IImplementer, ILocatable
	{
		IReadOnlyList<IVariableDeclaration> VariableDeclarations { get; }

		IReadOnlyList<IObjectProcDeclaration> ProcDeclarations { get; }
	}
}

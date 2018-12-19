using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IObjectDeclarationHolder: IIdentifiable, IImplementer
	{
		IReadOnlyList<IObjectVariableDeclaration> Variables { get; }

		IReadOnlyList<IObjectProcDeclaration> Procs { get; }
	}
}
using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface IObjectDeclarationHolder: IIdentifiable, IImplementer, IValidatable
	{
		IReadOnlyList<IObjectVariableDeclaration> Variables { get; }

		IReadOnlyList<IObjectProcDeclaration> Procs { get; }
	}
}
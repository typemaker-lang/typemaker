using System.Collections.Generic;
using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	public interface IInterface: IIdentifiable, IImplementer, ILocatable
	{
		IReadOnlyList<IVariableDeclaration> VariableDeclarations { get; }

		IReadOnlyList<IObjectProcDeclaration> ProcDeclarations { get; }
	}
}

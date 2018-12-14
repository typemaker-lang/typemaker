using System.Collections.Generic;

namespace Typemaker.Ast
{
	public interface IGenericDeclaration: IIdentifiable
	{
		IReadOnlyList<IImplements> Implements { get; }

		IReadOnlyList<IVarDeclaration> VarDeclarations { get; }

		IReadOnlyList<IProcDeclaration> ProcDeclarations { get; }
	}
}
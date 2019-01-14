using System.Collections.Generic;

namespace Typemaker.Ast
{
	public interface IGenericDeclaration: IIdentifiable
	{
		IEnumerable<IImplements> Implements { get; }

		IEnumerable<IVarDeclaration> VarDeclarations { get; }

		IEnumerable<IProcDeclaration> ProcDeclarations { get; }
	}
}
using System.Collections.Generic;

namespace Typemaker.Ast
{
	public interface IObjectDeclaration : IGenericDeclaration, IDecorated
	{
		IReadOnlyList<IAssigmentStatement> VarOverrides { get; }
	}
}

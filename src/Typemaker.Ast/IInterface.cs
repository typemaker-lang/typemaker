using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IInterface : IGenericDeclaration, IInterfaceImplementer
	{
		IReadOnlyList<IVarDeclaration> VarDeclarations { get; }
		IReadOnlyList<IProcDeclaration> ProcDeclarations { get; }
	}
}

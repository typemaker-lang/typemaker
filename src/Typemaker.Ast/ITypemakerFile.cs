using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface ITypemakerFile : ISyntaxTree
	{
		IReadOnlyList<IGlobalVarDeclaration> Globals { get; }
		IReadOnlyList<IGenericDeclaration> EnumsAndInterfaces { get; }
		IReadOnlyList<IGlobalProcDeclaration> Procs { get; }

		IReadOnlyList<IObjectDeclaration> Datums { get; }

		IReadOnlyList<IProcDefinition> DatumProcs { get; }
	}
}

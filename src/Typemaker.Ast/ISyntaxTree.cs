using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Typemaker.Ast.Trivia;

namespace Typemaker.Ast
{
	public interface ISyntaxTree : ISyntaxNode
	{
		string FilePath { get; }

		IReadOnlyList<IMapDeclaration> Maps { get; }

		IReadOnlyList<IGlobalVarDeclaration> Globals { get; }

		IReadOnlyList<IGenericDeclaration> EnumsAndInterfaces { get; }

		IReadOnlyList<IGlobalProcDeclaration> Procs { get; }

		IReadOnlyList<IObjectDeclaration> Datums { get; }

		IReadOnlyList<IObjectProcDefinition> DatumProcs { get; }
	}
}

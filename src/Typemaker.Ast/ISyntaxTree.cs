using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Typemaker.Ast
{
	public interface ISyntaxTree : ISyntaxNode
	{
		string FilePath { get; }

		IReadOnlyList<IMapDeclaration> Maps { get; }

		IReadOnlyList<IVarDeclaration> GlobalVars { get; }

		IReadOnlyList<IEnumDefinition> Enums { get; }

		IReadOnlyList<IInterface> Interfaces { get; }

		IReadOnlyList<IProcDeclaration> ProcDeclarations { get; }

		IReadOnlyList<IObjectDeclaration> Objects { get; }

		IReadOnlyList<IProcDefinition> ProcDefinitions { get; }
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Typemaker.Ast
{
	public interface ISyntaxTree : ISyntaxNode
	{
		string FilePath { get; }

		IEnumerable<IMapDeclaration> Maps { get; }

		IEnumerable<IVarDeclaration> GlobalVars { get; }

		IEnumerable<IEnumDefinition> Enums { get; }

		IEnumerable<IInterface> Interfaces { get; }

		IEnumerable<IProcDeclaration> ProcDeclarations { get; }

		IEnumerable<IObjectDeclaration> Objects { get; }

		IEnumerable<IProcDefinition> ProcDefinitions { get; }
	}
}

using System.Collections.Generic;
using Typemaker.Ast.Statements;

namespace Typemaker.Ast
{
	public interface IObjectDeclaration : IGenericDeclaration, IDecorated
	{
		IObjectPath DeclaredParentPath { get; }
		IReadOnlyList<ISetStatement> SetStatements { get; }
		IReadOnlyList<IAssignmentStatement> VarOverrides { get; }
	}
}

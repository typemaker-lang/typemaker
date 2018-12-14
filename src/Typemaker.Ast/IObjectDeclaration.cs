using System.Collections.Generic;
using Typemaker.Ast.Statements;
using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast
{
	public interface IObjectDeclaration : IGenericDeclaration, IDecorated
	{
		IObjectPath DeclaredParentPath { get; }
		IReadOnlyList<ISetStatement> SetStatements { get; }
		IReadOnlyList<IAssignment> VarOverrides { get; }
	}
}

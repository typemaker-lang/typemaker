using System.Collections.Generic;
using Typemaker.Ast.Statements;
using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast
{
	public interface IObjectDeclaration : IGenericDeclaration, IDecorated
	{
		IObjectPath DeclaredParentPath { get; }
		IEnumerable<ISetStatement> SetStatements { get; }
		IEnumerable<IAssignment> VarOverrides { get; }
	}
}

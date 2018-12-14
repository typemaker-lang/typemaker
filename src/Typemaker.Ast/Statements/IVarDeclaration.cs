using Typemaker.Ast.Statements;

namespace Typemaker.Ast
{
	public interface IVarDeclaration : IStatement, ITypedIdentifierDeclaration, IDecorated
	{
		bool IsConst { get; }
	}
}

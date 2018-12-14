using Typemaker.Ast.Statements;

namespace Typemaker.Ast
{
	public interface IVarDeclaration : IStatement, IIdentifierDeclaration, IDecorated
	{
		bool IsConst { get; }
	}
}

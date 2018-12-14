using Typemaker.Ast.Statements;

namespace Typemaker.Ast.Statements
{
	public interface IVarDefinition : IStatement, IIdentifierDeclaration
	{
		bool IsConst { get; }
	}
}

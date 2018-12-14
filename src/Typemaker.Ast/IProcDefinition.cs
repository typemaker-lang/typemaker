using Typemaker.Ast.Statements;

namespace Typemaker.Ast
{
	public interface IProcDefinition : IProcDeclaration
	{
		IStatement Body { get; }
	}
}

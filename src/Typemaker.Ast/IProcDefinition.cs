using Typemaker.Ast.Statements;

namespace Typemaker.Ast
{
	public interface IProcDefinition : ISyntaxNode
	{
		bool IsInline { get; }

		IStatement Body { get; }
	}
}

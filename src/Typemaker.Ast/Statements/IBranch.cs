using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast.Statements
{
	public interface IBranch : IStatement
	{
		IExpression Condition { get; }
	}
}

namespace Typemaker.Ast.Statements.Expressions
{
	public interface IExpression : IStatement
	{
		bool IsConstant { get; }

		RootType? EvaluateType();
	}
}

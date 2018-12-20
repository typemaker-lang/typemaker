namespace Typemaker.Ast.Statements.Expressions
{
	public interface IExpression : IStatement
	{
		bool IsConstant { get; }
		bool IsCompileTime { get; }

		RootType? EvaluateType();
	}
}

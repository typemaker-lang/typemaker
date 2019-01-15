namespace Typemaker.Ast.Statements.Expressions
{
	public interface IExpression : IStatement
	{
		/// <summary>
		/// If the expression can be evaluated at compile time by Typemaker
		/// </summary>
		bool IsConstant { get; }

		/// <summary>
		/// If the expression can be evaluated at compile time by Typemaker but not by DM
		/// </summary>
		bool IsCompileTime { get; }

		RootType? EvaluateType();
	}
}

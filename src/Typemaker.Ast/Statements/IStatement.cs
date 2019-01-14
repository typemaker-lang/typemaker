namespace Typemaker.Ast.Statements
{
	public interface IStatement : ISyntaxNode
	{
		bool BlockBreaker { get; }
		bool HasSideEffects { get; }
	}
}
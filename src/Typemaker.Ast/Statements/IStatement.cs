namespace Typemaker.Ast.Statements
{
	public interface IStatement : ISyntaxNode
	{
		bool HasSideEffects { get; }
	}
}
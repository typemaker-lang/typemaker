namespace Typemaker.Ast.Statements
{
	public interface IBodiedStatement : IStatement
	{
		IStatement Body { get; }
	}
}
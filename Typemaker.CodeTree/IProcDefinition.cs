namespace Typemaker.CodeTree
{
	public interface IProcDefinition : ILocatable
	{
		IProcDeclaration Declaration { get; }

		IStatementBlock Body { get; }
	}
}
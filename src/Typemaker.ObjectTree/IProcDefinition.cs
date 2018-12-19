namespace Typemaker.ObjectTree
{
	public interface IProcDefinition : ILocatable
	{
		IProcDeclaration Declaration { get; }

		IStatementBlock Body { get; }
	}
}
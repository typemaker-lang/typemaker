namespace Typemaker.ObjectTree
{
	public interface IProcDefinition : ILocatable
	{
		bool IsInline { get; }

		IProcDeclaration Declaration { get; }
	}
}
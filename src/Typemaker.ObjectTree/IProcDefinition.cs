namespace Typemaker.ObjectTree
{
	public interface IProcDefinition
	{
		bool IsInline { get; }

		IProcDeclaration Declaration { get; }
	}
}
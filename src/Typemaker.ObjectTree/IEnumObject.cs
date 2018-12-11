namespace Typemaker.CodeTree
{
	public interface IEnumObject : INameable, ILocatable
	{
		RootType? RootType { get; }

		string StringValue { get; }
		int? IntValue { get; }
	}
}
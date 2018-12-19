namespace Typemaker.ObjectTree
{
	public interface IEnumObject : INameable, ILocatable
	{
		RootType? RootType { get; }

		string StringValue { get; }
		int? IntValue { get; }
	}
}
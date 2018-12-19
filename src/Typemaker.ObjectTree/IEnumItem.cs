namespace Typemaker.ObjectTree
{
	public interface IEnumItem : IIdentifiable, ILocatable
	{
		string StringValue { get; }
		int? IntValue { get; }
	}
}
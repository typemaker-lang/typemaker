using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	public interface IEnumObject : IIdentifiable, ILocatable
	{
		string StringValue { get; }
		int? IntValue { get; }
	}
}
namespace Typemaker.Ast
{
	public interface IEnumItem : ISyntaxNode, IIdentifiable
	{
		bool AutoValue { get; }

		string StringValue { get; }

		int? IntValue { get; }
	}
}
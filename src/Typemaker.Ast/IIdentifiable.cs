namespace Typemaker.Ast
{
	public interface IIdentifiable : ISyntaxNode
	{
		string Name { get; }
	}
}

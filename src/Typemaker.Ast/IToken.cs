namespace Typemaker.Ast
{
	public interface IToken : ILocatable
	{
		TokenClass Class { get; }
		int Type { get; }
		string Text { get; }
	}
}
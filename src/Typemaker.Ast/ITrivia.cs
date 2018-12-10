namespace Typemaker.Ast
{
	public interface ITrivia
	{
		TriviaType TriviaType { get; }
		ITrivia Lead { get; }
		ITrivia Trail { get; }
	}
}
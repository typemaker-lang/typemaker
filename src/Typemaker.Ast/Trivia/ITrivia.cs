namespace Typemaker.Ast.Trivia
{
	public interface ITrivia
	{
		TriviaType TriviaType { get; }
		ITrivia Lead { get; }
		ITrivia Trail { get; }
		ILocation Start { get; }
		ILocation End { get; }
	}
}
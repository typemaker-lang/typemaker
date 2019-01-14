namespace Typemaker.Ast
{
	interface IInternalTrivia : ITrivia
	{
		new SyntaxNode Node { get; }
	}
}
using Antlr4.Runtime;

namespace Typemaker.Ast
{
	sealed class WhitespaceTrivia : SyntaxNode, IWhitespaceTrivia
	{
		public WhitespaceType Type { get; }

		public ulong Amount { get; }

		public WhitespaceTrivia(WhitespaceType type, SyntaxNode syntaxNode, ISyntaxTree tree, IToken token) : base(syntaxNode, tree, token)
		{
			Type = type;
			Amount = (ulong)token.Text.Length;
		}
	}
}
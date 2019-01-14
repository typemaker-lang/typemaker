using System;

namespace Typemaker.Ast
{
	class Trivia : ITrivia
	{
		public IToken Token { get; }

		public ISyntaxNode Node { get; }

		public Location Start => Node?.Start ?? Token.Start;

		public Location End => Node?.End ?? Token.End;
		
		public Trivia(IToken token)
		{
			Token = token ?? throw new ArgumentNullException(nameof(token));
		}

		public Trivia(ISyntaxNode syntaxNode)
		{
			Node = syntaxNode ?? throw new ArgumentNullException(nameof(syntaxNode));
		}
	}
}
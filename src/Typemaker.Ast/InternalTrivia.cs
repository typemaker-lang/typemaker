using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	sealed class InternalTrivia : Trivia, IInternalTrivia
	{
		new public SyntaxNode Node { get; }

		public InternalTrivia(Antlr4.Runtime.IToken token) : base(new Token(token ?? throw new ArgumentNullException(nameof(token))))
		{ }

		public InternalTrivia(SyntaxNode syntaxNode) : base(syntaxNode)
		{
			Node = syntaxNode;
		}
	}
}

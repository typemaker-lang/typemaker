using System;
using System.Collections.Generic;

namespace Typemaker.Ast.Tests
{
	interface ISyntaxNodeValidator
	{
		void Validate(ITrivia trivia, bool validateTokens);
		void Validate(ISyntaxNode syntaxNode, bool validateTokens);
	}
}
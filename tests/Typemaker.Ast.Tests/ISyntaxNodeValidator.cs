using System;
using System.Collections.Generic;

namespace Typemaker.Ast.Tests
{
	interface ISyntaxNodeValidator
	{
		void Validate(ISyntaxNode syntaxNode, bool validateTrivia);
	}
}
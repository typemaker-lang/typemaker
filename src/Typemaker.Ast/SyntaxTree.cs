using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public sealed class SyntaxTree : ISyntaxTree
	{
		public string FilePath => throw new NotImplementedException();

		public ISyntaxTree Tree => throw new NotImplementedException();

		public ISyntaxNode Parent => throw new NotImplementedException();

		public ILocation Start => throw new NotImplementedException();

		public ILocation End => throw new NotImplementedException();

		public IReadOnlyList<ISyntaxNode> Children => throw new NotImplementedException();

		public string Syntax => throw new NotImplementedException();

		public ITrivia Lead => throw new NotImplementedException();

		public ITrivia Trail => throw new NotImplementedException();

		public TriviaType TriviaType => throw new NotImplementedException();
	}
}

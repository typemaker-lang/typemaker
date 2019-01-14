using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Typemaker.Ast
{
	abstract class SyntaxNode : ISyntaxNode
	{
		public ISyntaxTree Tree { get; private set; }

		public ISyntaxNode Parent { get; private set; }

		public IEnumerable<IToken> Tokens => Trivia.SelectMany(x => SelectTokens(x));

		public IReadOnlyList<ITrivia> Trivia { get; }

		public IEnumerable<ISyntaxNode> Children => Trivia.Where(x => x.Node != null).Select(x => x.Node);

		public ILocatable TriviaRestrictionViolation { get; private set; }

		public Location Start => Trivia.First().Start;

		public Location End => Trivia.Last().End;

		static IEnumerable<IToken> SelectTokens(ITrivia trivia)
		{
			if (trivia.Token != null)
				yield return trivia.Token;
			else
				foreach (var I in trivia.Node.Tokens)
					yield return I;
		}

		protected SyntaxNode(IEnumerable<ITrivia> trivia)
		{
			Trivia = trivia?.ToList() ?? throw new ArgumentNullException(nameof(trivia));
		}

		public void LinkTree(ISyntaxNode parent, ISyntaxTree tree)
		{
			if (parent == null)
				throw new ArgumentNullException(nameof(parent));
			if (tree == null)
				throw new ArgumentNullException(nameof(tree));
			if (Tree != null)
				throw new InvalidOperationException("Tree has already been set!");
			if (Parent != null)
				throw new InvalidOperationException("Parent has already been set!");

			Tree = tree;
			Parent = parent;

			foreach (var I in Trivia.Where(x => x.Node != null).Select(x => x.Node))
				I.LinkTree(this, tree);
		}

		protected TChildNode ChildAs<TChildNode>(int index = 0) where TChildNode : ISyntaxNode => ChildrenAs<TChildNode>().ElementAt(index);

		public IEnumerable<TChildNode> ChildrenAs<TChildNode>() where TChildNode : ISyntaxNode => Children.Where(x => x is TChildNode).Select(x => (TChildNode)(object)x);
	}
}
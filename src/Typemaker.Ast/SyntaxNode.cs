using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Typemaker.Ast
{
	abstract class SyntaxNode : ISyntaxNode
	{
		public ISyntaxTree Tree { get; private set; }

		public ISyntaxNode Parent => parent;

		public IEnumerable<ITrivia> Trivia => childTrivia.Select(x => x);

		public IEnumerable<ISyntaxNode> Children => childTrivia.Where(x => x.Node != null).Select(x => x.Node);

		public ILocatable TriviaRestrictionViolation { get; private set; }

		public Location Start => childTrivia.First().Start;

		public Location End => childTrivia.Last().End;

		readonly List<IInternalTrivia> childTrivia;

		SyntaxNode parent;

		protected SyntaxNode(ParserRuleContext context, IEnumerable<IInternalTrivia> childTrivia)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			this.childTrivia = childTrivia?.ToList() ?? throw new ArgumentNullException(nameof(childTrivia));
		}

		protected void LinkTree(SyntaxNode parent, ISyntaxTree tree)
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
			this.parent = parent;

			foreach (var I in childTrivia.Where(x => x.Node != null).Select(x => x.Node))
				I.LinkTree(this, tree);
		}

		protected TChildNode ChildAs<TChildNode>(int index = 0) where TChildNode : ISyntaxNode => ChildrenAs<TChildNode>().ElementAt(index);

		public IEnumerable<TChildNode> ChildrenAs<TChildNode>() where TChildNode : ISyntaxNode => Children.Where(x => x is TChildNode).Select(x => (TChildNode)(object)x);
	}
}
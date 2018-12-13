using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Typemaker.Ast.Trivia;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	abstract class SyntaxNode : ISyntaxNode
	{
		public ISyntaxTree Tree { get; private set; }

		public ISyntaxNode Parent { get; private set; }

		public IReadOnlyList<ISyntaxNode> Children => children;

		public string Syntax { get; }
		
		public ILocation Start { get; }

		public ILocation End { get; }
		
		public bool Trivia { get; }

		public IReadOnlyList<ICommentTrivia> Comments => ChildrenAs<ICommentTrivia>();

		public IReadOnlyList<IWhitespaceTrivia> Whitespace => ChildrenAs<IWhitespaceTrivia>();

		readonly List<SyntaxNode> children;

		readonly int startTokenIndex;
		readonly int stopTokenIndex;

		protected SyntaxNode(ParserRuleContext context, IEnumerable<SyntaxNode> children)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			this.children = children?.ToList() ?? throw new ArgumentNullException(nameof(children));

			Syntax = context.GetText();

			startTokenIndex = context.Start.TokenIndex;
			stopTokenIndex = context.Stop.TokenIndex;

			Start = new Location(context.Start, false);
			End = new Location(context.Stop, true);
		}

		protected SyntaxNode(SyntaxNode parent, ISyntaxTree tree, IToken token)
		{
			Parent = parent ?? throw new ArgumentNullException(nameof(parent));
			Tree = tree ?? throw new ArgumentNullException(nameof(tree));
			Syntax = token?.Text ?? throw new ArgumentNullException(nameof(token));
			Start = new Location(token, false);
			End = new Location(token, true);

			Trivia = true;
		}

		protected void BuildTrivia(SyntaxNode left, SyntaxNode right, ISyntaxTree tree, IList<IToken> tokens)
		{
			if (tree == null)
				throw new ArgumentNullException(nameof(tree));
			if (tokens == null)
				throw new InvalidOperationException(nameof(tokens));
			if (Tree != null)
				throw new InvalidOperationException("Tree has already been set!");

			Tree = tree;

			SyntaxNode childLeft = left;
			for (var I = 0; I < children.Count; ++I)
			{
				var child = children[I];
				var childRight = I < children.Count - 1 ? children[I + 1] : right;
				child.BuildTrivia(childLeft, childRight, tree, tokens);
				childLeft = child;
			}
			
			var leftmost = (left?.stopTokenIndex ?? -1) + 1;
			var rightmost = (right?.startTokenIndex ?? tokens.Count);
			var offset = 0;

			int RecheckSkipRange() => offset >= children.Count ? rightmost : children[offset].startTokenIndex;
			var check = RecheckSkipRange();

			for(var I = leftmost; I < rightmost; ++I)
			{
				if(I == check)
				{
					I = children[offset].stopTokenIndex + 1;
					++offset;
					check = RecheckSkipRange();
					continue;
				}

				SyntaxNode newNode;
				var token = tokens[I];
				switch (token.Type)
				{
					case TypemakerLexer.NEWLINES:
						newNode = new WhitespaceTrivia(WhitespaceType.Newlines, this, tree, token);
						break;
					case TypemakerLexer.TABS:
						newNode = new WhitespaceTrivia(WhitespaceType.Tabs, this, tree, token);
						break;
					case TypemakerLexer.SPACES:
						newNode = new WhitespaceTrivia(WhitespaceType.Spaces, this, tree, token);
						break;
					case TypemakerLexer.SINGLE_LINE_COMMENT:
						newNode = new CommentTrivia(this, tree, token, false);
						break;
					case TypemakerLexer.DELIMITED_COMMENT:
						newNode = new CommentTrivia(this, tree, token, true);
						break;
					default:
						continue;
						throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Invalid trivia token {0} ({1})!", token.Type, token.Text));
				}

				children.Insert(offset, newNode);
				++offset;
			}
		}

		protected IReadOnlyList<TChildNode> ChildrenAs<TChildNode>() where TChildNode : ISyntaxNode => children.Where(x => x is TChildNode).Select(x => (TChildNode)(object)x).ToList();
	}
}
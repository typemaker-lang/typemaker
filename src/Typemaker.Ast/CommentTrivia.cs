using Antlr4.Runtime;
using System.Linq;

namespace Typemaker.Ast
{
	sealed class CommentTrivia : SyntaxNode, ICommentTrivia
	{
		public ulong? LineCount {get;}

		public string Comment { get; }

		public CommentTrivia(SyntaxNode parent, ISyntaxTree tree, IToken token, bool multiLine) : base(parent, tree, token)
		{
			var text = token.Text;
			if (multiLine)
			{
				LineCount = (ulong)text.Where(x => x == '\n').Count() + 1;
				Comment = text.Substring(2, text.Length - 4);
			}
			else
				Comment = text.Substring(2);
		}
	}
}
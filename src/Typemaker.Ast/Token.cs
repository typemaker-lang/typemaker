using System;
using System.Diagnostics;
using Typemaker.Parser;

namespace Typemaker.Ast
{
[DebuggerDisplay("{DebuggerDisplay,nq}")]
	sealed class Token : IToken
	{
		public TokenClass Class { get; }

		public int Type => token.Type;

		public string Text => token.Text;

		public Location Start { get; }

		public Location End { get; }

		readonly Antlr4.Runtime.IToken token;

		public Token(Antlr4.Runtime.IToken token)
		{
			this.token = token ?? throw new ArgumentNullException(nameof(token));
			
			switch (Type)
			{
				case TypemakerLexer.NEWLINES:
					Class = TokenClass.NewLines;
					break;
				case TypemakerLexer.WINDOWS_NEWLINES:
					Class = TokenClass.WindowsNewLines;
					break;
				case TypemakerLexer.TABS:
					Class = TokenClass.Tabs;
					break;
				case TypemakerLexer.SPACES:
					Class = TokenClass.Spaces;
					break;
				case TypemakerLexer.SINGLE_LINE_COMMENT:
					Class = TokenClass.SingleLineComment;
					break;
				case TypemakerLexer.DELIMITED_COMMENT:
					Class = TokenClass.MultiLineComment;
					break;
				default:
					Class = TokenClass.Grammar;
					break;
			}

			Start = new Location
			{
				Column = (ulong)token.Column,
				Line = (ulong)token.Line
			};
			End = new Location
			{
				Column = (ulong)(token.Column + Text.Length),
				Line = (ulong)token.Line
			};
		}
		private string DebuggerDisplay => $"{Text} ({TypemakerLexer.DefaultVocabulary.GetSymbolicName(Type)})";
	}
}

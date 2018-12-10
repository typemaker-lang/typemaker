using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Typemaker.Parser.Tests
{
	public sealed class TestLexer
	{
		static void SourceMatchTokens(string source, List<int> expectedTokens)
		{
			using (var ss = new StringReader(source))
			{
				var input = new AntlrInputStream(ss);
				var lexer = new TypemakerLexer(input);
				var tokenInterfaces = lexer.GetAllTokens();
				var actualTokens = tokenInterfaces.Select(x => x.Type).ToList();
				var translatedExpected = expectedTokens.Select(x => lexer.Vocabulary.GetSymbolicName(x)).ToList();
				var translatedActual = actualTokens.Select(x => lexer.Vocabulary.GetSymbolicName(x)).ToList();
				Assert.Equal(expectedTokens.Count, actualTokens.Count);
				for (var I = 0; I < expectedTokens.Count; ++I)
					Assert.True(actualTokens[I] == expectedTokens[I], "Expected " + translatedExpected[I] + " got " + translatedActual[I]);
			}
		}

		[Fact]
		public void TestEmbeddedExpressions1()
		{
			//each double double quote is only one
			const string Source = @"/proc/main() -> void {
var/string/test = ""I[""[""am""] [{""
a very[""very""]
@{
"" good \""}
""}] embedded""] string"";
}";
			SourceMatchTokens(Source, new List<int> {
				TypemakerLexer.SLASH,
 				TypemakerLexer.PROC,
 				TypemakerLexer.SLASH,
 				TypemakerLexer.IDENTIFIER,
 				TypemakerLexer.LPAREN,
 				TypemakerLexer.RPAREN,
 				TypemakerLexer.SPACES,
 				TypemakerLexer.RDEC,
 				TypemakerLexer.SPACES,
 				TypemakerLexer.VOID,
 				TypemakerLexer.SPACES,
 				TypemakerLexer.LCURL,
 				TypemakerLexer.NEWLINES,
 				TypemakerLexer.VAR,
 				TypemakerLexer.SLASH,
 				TypemakerLexer.STRING,
 				TypemakerLexer.SLASH,
 				TypemakerLexer.IDENTIFIER,
 				TypemakerLexer.SPACES,
 				TypemakerLexer.EQUALS,
 				TypemakerLexer.SPACES,
 				TypemakerLexer.STRING_START,
 				TypemakerLexer.STRING_INSIDE,
 				TypemakerLexer.EMBED_START,
 				TypemakerLexer.STRING_START,
 				TypemakerLexer.EMBED_START,
 				TypemakerLexer.STRING_START,
 				TypemakerLexer.STRING_INSIDE,
 				TypemakerLexer.STRING_CLOSE,
 				TypemakerLexer.RBRACE,
 				TypemakerLexer.STRING_INSIDE,
 				TypemakerLexer.EMBED_START,
 				TypemakerLexer.MULTI_STRING_START,
 				TypemakerLexer.MULTI_STRING_INSIDE,
 				TypemakerLexer.EMBED_START,
 				TypemakerLexer.STRING_START,
 				TypemakerLexer.STRING_INSIDE,
 				TypemakerLexer.STRING_CLOSE,
 				TypemakerLexer.RBRACE,
 				TypemakerLexer.MULTI_STRING_INSIDE,
				//The quote breaks the token in two
				//nbd since the parser eats them all anyway
 				TypemakerLexer.MULTI_STRING_INSIDE,
 				TypemakerLexer.CHAR_INSIDE,
 				TypemakerLexer.MULTI_STRING_INSIDE,
				TypemakerLexer.MULTI_STRING_CLOSE,
 				TypemakerLexer.RBRACE,
 				TypemakerLexer.STRING_INSIDE,
 				TypemakerLexer.STRING_CLOSE,
 				TypemakerLexer.RBRACE,
 				TypemakerLexer.STRING_INSIDE,
 				TypemakerLexer.STRING_CLOSE,
				TypemakerLexer.SEMI,
				TypemakerLexer.NEWLINES,
				TypemakerLexer.RCURL
			});
		}
	}
}

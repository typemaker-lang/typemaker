using Antlr4.Runtime;
using System;
using System.IO;
using System.Linq;
using Typemaker.Parser;

namespace Typemaker.Compiler
{
    public class TestParser
    {
        public static void Main(string[] args)
		{
			using (FileStream fs = new FileStream("../../../../Typemaker.Compiler/test.tm", FileMode.Open, FileAccess.Read))
			{
				var input = new AntlrInputStream(fs);
				var lexer = new TypemakerLexer(input);
				var tokens = lexer.GetAllTokens();
				var tokenNames = String.Join(" ", tokens.Select(x => lexer.Vocabulary.GetDisplayName(x.Type)));
				Console.WriteLine(tokenNames);
				Console.ReadKey();
			}
        }
    }
}

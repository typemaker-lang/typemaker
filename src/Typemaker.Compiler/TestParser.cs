using Antlr4.Runtime;
using System;
using System.IO;
using Typemaker.Parser;

namespace Typemaker.Compiler
{
    public class TestParser
    {
        public static void Main(string[] args)
		{
			using (FileStream fs = new FileStream("../../../libdm/1458/public/client.tm", FileMode.Open, FileAccess.Read))
			{
				var input = new AntlrInputStream(fs);
				var lexer = new TypemakerLexer(input);
				var tokens = new CommonTokenStream(lexer);
				var parser = new TypemakerParser(tokens);
				var tree = parser.compilation_unit();
				var allTokens = lexer.GetAllTokens();
				Console.WriteLine(tree.ToStringTree());
				Console.ReadKey();
			}
        }
    }
}

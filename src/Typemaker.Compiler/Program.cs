using System;
using System.IO;

namespace Typemaker.Compiler
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var fs = new FileStream("../../../test.tm", FileMode.Open, FileAccess.Read))
			{
				var lexer = new Parser.TypemakerLexer(new Antlr4.Runtime.AntlrInputStream(fs));
				var tokens = lexer.GetAllTokens();
			}
		}
	}
}

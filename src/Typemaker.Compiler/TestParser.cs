using System.Collections.Generic;
using System.IO;
using Typemaker.Ast;

namespace Typemaker.Compiler
{
	public class TestParser
	{
		public static void Main(string[] args)
		{
			const string Path = "../../../../Typemaker.Compiler/test.tm";
			ISyntaxTree tree;
			IReadOnlyList<ParseError> errors;
			using (FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
				tree = SyntaxTreeFactory.CreateSyntaxTree(fs, Path, out errors);
			int i = 0;
		}
	}
}

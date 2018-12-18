﻿using System.Collections.Generic;
using System.IO;
using Typemaker.Ast;

namespace Typemaker.Compiler
{
	public class TestParser
	{
		public static void Main(string[] args)
		{
			Settings.SettingsFactory.DeserializeSettings("../../../../Typemaker.Compiler/libdm/typemaker.1458.public.json", null).GetAwaiter().GetResult();

			const string Path = "../../../../Typemaker.Compiler/test.tm";
			ISyntaxTree tree;
			IReadOnlyList<ParseError> errors;
			using (FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
				tree = SyntaxTreeFactory.Default.CreateSyntaxTree(fs, Path, out errors);
			int i = 0;
		}
	}
}

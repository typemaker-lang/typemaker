using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typemaker.Ast.Validation;
using Typemaker.Compiler.Settings;
using Xunit;

namespace Typemaker.Compiler.Tests
{
	public class TestCompiler
	{
		[Fact]
		public async Task TestCompile1458()
		{
			var compiler = new Compiler(new FilePathProvider(), new SyntaxTreeValidator());

			var result = await compiler.Compile(new CodeSearchSettings
			{
				Root = "../../../../../src/Typemaker.Compiler/libdm/1458",
				Ignore = new List<string> { "strong" }
			}, default);
			int i = 0;
		}
	}
}

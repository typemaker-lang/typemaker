using Typemaker.Ast;

namespace Typemaker.Compiler
{
	public class CompilerError
	{
		public ErrorClass ErrorClass { get; set; }

		public string FilePath { get; set; }

		public string Code { get; set; }

		public string Message { get; set; }

		public ILocatable Location { get; set; }
	}
}
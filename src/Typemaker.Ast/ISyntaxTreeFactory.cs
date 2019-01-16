using System.Collections.Generic;
using System.IO;

namespace Typemaker.Ast
{
	public interface ISyntaxTreeFactory
	{
		ISyntaxTree CreateSyntaxTree(Stream file, string filePath, bool disposeStream, out IReadOnlyList<ParseError> parseErrors);
	}
}
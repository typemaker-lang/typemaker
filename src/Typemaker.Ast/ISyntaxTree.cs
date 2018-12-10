using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface ISyntaxTree : ISyntaxNode
	{
		string FilePath { get; }
	}
}

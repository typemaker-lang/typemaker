using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	interface IObjectPath : ISyntaxNode
	{
		string ExtendedPath { get; }
	}
}

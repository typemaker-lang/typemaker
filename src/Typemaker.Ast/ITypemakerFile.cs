using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	interface ITypemakerFile
	{
		string FilePath { get; set; }
		List<INode> Nodes { get; set; }
	}
}

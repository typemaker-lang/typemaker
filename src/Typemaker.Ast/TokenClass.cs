using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public enum TokenClass
	{
		Grammar,
		Tabs,
		Spaces,
		NewLines,
		WindowsNewLines,
		SingleLineComment,
		MultiLineComment
	}
}

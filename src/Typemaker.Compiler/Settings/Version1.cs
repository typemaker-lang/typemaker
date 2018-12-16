using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Compiler.Settings
{
	public sealed class Version1 : TypemakerSettings
	{
		string Extends { get; set; }
		string CodeRoot { get; set; }
		string OutputDirectory { get; set; }
		ByondVersionDefinition ByondVersion { get; set; }
	}
}
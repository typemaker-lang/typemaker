using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Compiler.Settings
{
	public sealed class LinterSettings
	{
		public bool? EnforceTabs { get; set; }
		
		public bool? AllmanBraces { get; set; }
		
		public bool? NoOperatorOverloading { get; set; }
		
		public bool? NoSingleLineBlocks { get; set; }
	}
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Compiler.Settings
{
	public sealed class ByondVersion
	{
		public long Major { get; set; }
		
		public long Minor { get; set; }
	}
}
using System;

namespace Typemaker.Compiler
{
	public abstract class TypemakerSettings
	{
		public int? Version { get; set; }
		public string Extends { get; set; }
	}
}
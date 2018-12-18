using System;
using System.IO;

namespace Typemaker.Compiler
{
	public abstract class TypemakerSettings : IPathSettingsNode
	{
		public int? Version { get; set; }
		public string Extends { get; set; }

		public virtual void FixPaths(string relativePath)
		{
			if (relativePath == null)
				throw new ArgumentNullException(nameof(relativePath));
			if (Extends != null && !Path.IsPathRooted(Extends))
				Extends = Path.GetFullPath(Path.Combine(relativePath, Extends));
		}
	}
}
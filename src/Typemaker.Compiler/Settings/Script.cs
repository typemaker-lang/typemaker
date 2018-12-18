using System;
using System.IO;

namespace Typemaker.Compiler.Settings
{
	public sealed class Script : IPathSettingsNode
	{
		public string Windows { get; set; }
		public string Linux { get; set; }

		public void FixPaths(string relativePath)
		{
			if (relativePath == null)
				throw new ArgumentNullException(nameof(relativePath));

			if (Windows != null && !Path.IsPathRooted(Windows))
				Windows = Path.Combine(relativePath, Windows);

			if (Linux != null && !Path.IsPathRooted(Linux))
				Linux = Path.Combine(relativePath, Linux);
		}
	}
}
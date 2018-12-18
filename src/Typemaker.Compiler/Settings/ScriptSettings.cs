using System;

namespace Typemaker.Compiler.Settings
{
	public sealed class ScriptSettings : IPathSettingsNode
	{
		public Script PreTranspile { get; set; }
		public Script PreCompile { get; set; }
		public Script PostCompile { get; set; }

		public void FixPaths(string relativePath)
		{
			if (relativePath == null)
				throw new ArgumentNullException(relativePath);

			PreTranspile?.FixPaths(relativePath);
			PreCompile?.FixPaths(relativePath);
			PreCompile?.FixPaths(relativePath);
		}
	}
}
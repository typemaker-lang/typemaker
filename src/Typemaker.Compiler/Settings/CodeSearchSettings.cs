using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Typemaker.Compiler.Settings
{
	public sealed class CodeSearchSettings : IPathSettingsNode
	{
		public string Root { get; set; }

		public List<string> Ignore { get; set; }

		public void FixPaths(string relativePath)
		{
			if (relativePath == null)
				throw new ArgumentNullException(nameof(relativePath));
			if (Root != null && !Path.IsPathRooted(Root))
				Root = Path.Combine(relativePath, Root);
			if(Ignore != null)
				Ignore = Ignore.Select(x => Path.IsPathRooted(x) ? x : Path.GetFullPath(Path.Combine(relativePath, x))).ToList();
		}
	}
}

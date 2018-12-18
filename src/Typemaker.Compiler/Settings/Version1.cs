using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Typemaker.Compiler.Settings
{
	public sealed class Version1 : TypemakerSettings
	{
		public CodeSearchSettings Include { get; set; }
		public string OutputDirectory { get; set; }
		public ByondVersionDefinition ByondVersion { get; set; }
		public bool? Library { get; set; }
		public List<string> Libraries { get; set; }

		[JsonProperty("strong_libdm")]
		public bool? StrongLibDM { get; set; }

		public bool? Debug { get; set; }

		public ScriptSettings Scripts { get; set; }

		public string Dme { get; }

		public LinterSettings LinterSettings { get; set; }

		public override void FixPaths(string relativePath)
		{
			base.FixPaths(relativePath);
			Include.FixPaths(relativePath);
			Scripts.FixPaths(relativePath);
			if (OutputDirectory != null && !Path.IsPathRooted(OutputDirectory))
				OutputDirectory = Path.GetFullPath(Path.Combine(relativePath, OutputDirectory));
			if (Libraries != null)
				Libraries = Libraries.Select(x => Path.IsPathRooted(x) ? x : Path.GetFullPath(Path.Combine(relativePath, x))).ToList();
		}
	}
}
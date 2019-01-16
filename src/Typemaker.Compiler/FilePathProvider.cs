using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Typemaker.Compiler.Settings;

namespace Typemaker.Compiler
{
	sealed class FilePathProvider : IFilePathProvider
	{
		public IReadOnlyList<string> GetCompilationUnitPaths(CodeSearchSettings settings, CancellationToken cancellationToken)
		{
			if (settings == null)
				throw new ArgumentNullException(nameof(settings));

			var root = settings.Root;
		
			IEnumerable<string> CheckDirectory(string addition)
			{
				var currentDirectory = Path.Combine(root, addition);

				var dirInfo = new DirectoryInfo(currentDirectory);

				foreach (var I in dirInfo.GetDirectories())
					if (!settings.Ignore.Any(x => x == I.Name))
						foreach (var J in CheckDirectory(Path.Combine(addition, I.Name)))
							yield return J;

				foreach (var I in dirInfo.GetFiles("*.tm"))
					if (!settings.Ignore.Any(x => x == I.Name))
						yield return I.FullName;
			}

			try
			{
				return CheckDirectory(String.Empty).ToList();
			}
			catch (IOException)
			{
				return null;
			}
		}
	}
}

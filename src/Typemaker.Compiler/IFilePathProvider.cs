using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Typemaker.Compiler.Settings;

namespace Typemaker.Compiler
{
	interface IFilePathProvider
	{
		IReadOnlyList<string> GetCompilationUnitPaths(CodeSearchSettings settings, CancellationToken cancellationToken);
	}
}

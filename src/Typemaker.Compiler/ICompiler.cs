using System.Threading;
using System.Threading.Tasks;
using Typemaker.Compiler.Settings;

namespace Typemaker.Compiler
{
	public interface ICompiler
	{
		Task<ICompileResult> Compile(CodeSearchSettings settings, CancellationToken cancellationToken);
	}
}

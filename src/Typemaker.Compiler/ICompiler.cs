using System.Threading;
using System.Threading.Tasks;

namespace Typemaker.Compiler
{
	public interface ICompiler
	{
		Task Compile(CompilerOptions compilerOptions, CancellationToken cancellationToken);
	}
}

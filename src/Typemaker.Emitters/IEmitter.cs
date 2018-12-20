using System.Threading;
using System.Threading.Tasks;
using Typemaker.Ast;

namespace Typemaker.Emitters
{
	public interface IEmitter
	{
		Task Emit(string filePath, ISyntaxTree syntaxTree, CancellationToken cancellationToken);
	}
}

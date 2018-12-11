using Typemaker.Ast;
using Typemaker.Ast.Trivia;

namespace Typemaker.CodeTree
{
	public interface ILocatable
	{
		string FilePath { get; }

		ILocation Location { get; }
	}
}
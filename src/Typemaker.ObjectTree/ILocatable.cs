using Typemaker.Ast;

namespace Typemaker.CodeTree
{
	public interface ILocatable
	{
		string FilePath { get; }

		Location Location { get; }
	}
}
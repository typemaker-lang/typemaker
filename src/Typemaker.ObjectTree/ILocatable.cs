using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	public interface ILocatable
	{
		string FilePath { get; }

		Location Location { get; }
	}
}
using Typemaker.Ast;

namespace Typemaker.CodeTree
{
	public interface ILocatable
	{
		string FilePath { get; }

		ILocation Location { get; }
	}
}
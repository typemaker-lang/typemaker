using Typemaker.Ast;

namespace Typemaker.CodeTree
{
	public interface ILocatable
	{
		IFileLocation FileLocation { get; }
	}
}
namespace Typemaker.ObjectTree
{
	public interface ILocatable : Ast.ILocatable
	{
		string FilePath { get; }
	}
}
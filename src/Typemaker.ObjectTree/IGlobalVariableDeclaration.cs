namespace Typemaker.ObjectTree
{
	public interface IGlobalVariableDeclaration : IVariableDeclaration
	{
		bool IsDeclared { get; }
	}
}
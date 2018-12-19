namespace Typemaker.ObjectTree
{
	public interface IObjectVariableDeclaration : IVariableDeclaration, IObjectDeclaration
	{
		bool IsReadonly { get; }
	}
}

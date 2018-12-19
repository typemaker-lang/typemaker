namespace Typemaker.ObjectTree
{
	public interface IObjectVariableDeclaration : IVariableDeclaration, IProtectable
	{
		bool IsReadonly { get; }
	}
}

namespace Typemaker.CodeTree
{
	public interface IObjectVariableDeclaration : IVariableDeclaration, IProtectable
	{
		bool IsReadonly { get; }
	}
}

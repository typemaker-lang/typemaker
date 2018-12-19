namespace Typemaker.ObjectTree
{
	public interface IArgumentDeclaration
	{
		bool IsVariadic { get; }

		IVariableDeclaration VariableDeclaration { get; }
	}
}
namespace Typemaker.CodeTree
{
	public interface IArgumentDeclaration
	{
		bool IsVariadic { get; }

		IVariableDeclaration VariableDeclaration { get; }
	}
}
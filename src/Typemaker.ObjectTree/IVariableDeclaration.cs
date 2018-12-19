namespace Typemaker.ObjectTree
{
	public interface IVariableDeclaration : INameable, ILocatable, IDeclarable
	{
		bool IsConst { get; }

		IExpression Initializer { get; }

		ITypeDeclaration TypeDeclaration { get; }
	}
}
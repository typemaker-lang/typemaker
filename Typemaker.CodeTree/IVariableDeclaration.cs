namespace Typemaker.CodeTree
{
	public interface IVariableDeclaration : INameable, ILocatable, IDeclarable
	{
		bool IsConst { get; }

		IExpression Initializer { get; }

		ITypeDeclaration TypeDeclaration { get; }
	}
}
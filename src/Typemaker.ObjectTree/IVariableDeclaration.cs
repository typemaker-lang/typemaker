namespace Typemaker.ObjectTree
{
	public interface IVariableDeclaration : IIdentifiable, ILocatable
	{
		bool IsConst { get; }

		ITypeDeclaration TypeDeclaration { get; }
	}
}
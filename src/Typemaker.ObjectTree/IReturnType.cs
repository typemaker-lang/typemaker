namespace Typemaker.ObjectTree
{
	public interface IReturnType
	{
		bool IsVoid { get; }

		ITypeDeclaration TypeDeclaration { get; }
	}
}
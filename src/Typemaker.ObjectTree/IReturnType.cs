namespace Typemaker.CodeTree
{
	public interface IReturnType
	{
		bool IsVoid { get; }

		ITypeDeclaration TypeDeclaration { get; }
	}
}
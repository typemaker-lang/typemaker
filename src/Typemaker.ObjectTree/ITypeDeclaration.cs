namespace Typemaker.ObjectTree
{
	public interface ITypeDeclaration
	{
		bool IsUnknown { get; }

		bool IsNullable { get; }

		RootType? RootType { get; }

		string ObjectIdentifier { get; }
	}
}

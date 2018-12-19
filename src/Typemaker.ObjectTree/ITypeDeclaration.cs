using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	public interface ITypeDeclaration
	{
		bool IsNullable { get; }

		RootType? RootType { get; }

		IObject Typepath { get; }

		ITypeDeclaration IndexType { get; }
		ITypeDeclaration MapType { get; }
	}
}

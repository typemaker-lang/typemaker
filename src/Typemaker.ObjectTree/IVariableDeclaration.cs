using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	public interface IVariableDeclaration : IIdentifiable, ILocatable, IDeclarable
	{
		bool IsConst { get; }

		ITypeDeclaration TypeDeclaration { get; }
	}
}
namespace Typemaker.CodeTree
{
	public interface IObjectProcDeclaration : IProcDeclaration, IProtectable
	{
		bool IsVirtual { get; }

		bool IsStatic { get; }
	}
}

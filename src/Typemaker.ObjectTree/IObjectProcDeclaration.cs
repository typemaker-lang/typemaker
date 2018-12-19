namespace Typemaker.ObjectTree
{
	public interface IObjectProcDeclaration : IProcDeclaration, IProtectable
	{
		bool IsVirtual { get; }

		bool IsStatic { get; }
	}
}

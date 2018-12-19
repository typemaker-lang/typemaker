namespace Typemaker.ObjectTree
{
	public interface IObjectProcDeclaration : IProcDeclaration, IObjectDeclaration
	{
		bool IsVirtual { get; }

		IObject Owner { get; }
	}
}

namespace Typemaker.ObjectTree
{
	public interface IObjectProcDeclaration : IProcDeclaration, IObjectDeclaration
	{
		bool IsVirtual { get; }

		new IObjectProcDefinition Definition { get; }
	}
}

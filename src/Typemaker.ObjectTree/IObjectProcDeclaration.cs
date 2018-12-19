namespace Typemaker.ObjectTree
{
	public interface IObjectProcDeclaration : IProcDeclaration, IObjectDeclaration
	{
		bool IsVirtual { get; }

		IObject Owner { get; }

		new IObjectProcDefinition Definition { get; }

		IObjectProcDeclaration FixParentChainAfterFileRemoval(string filePath);
	}
}

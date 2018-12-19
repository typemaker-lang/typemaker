namespace Typemaker.ObjectTree
{
	public interface IConstructor: IObjectProcDeclaration, IObjectProcDefinition
	{
		new IConstructor Parent { get; }
		new IConstructor Declaration { get; }
		new IConstructor Definition { get; }
		new IConstructor FixParentChainAfterFileRemoval(string filePath);
	}
}

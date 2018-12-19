namespace Typemaker.ObjectTree
{
	public interface IConstructor: IObjectProcDeclaration, IObjectProcDefinition
	{
		new IConstructor Parent { get; }
	}
}

namespace Typemaker.ObjectTree
{
	public interface IObjectDeclaration
	{
		IObject Object { get; }

		ProtectionLevel ProtectionLevel { get; }

		bool IsStatic { get; }
	}
}
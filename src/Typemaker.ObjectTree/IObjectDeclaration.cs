namespace Typemaker.ObjectTree
{
	public interface IObjectDeclaration
	{
		ProtectionLevel ProtectionLevel { get; }

		bool IsStatic { get; }
	}
}
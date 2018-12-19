namespace Typemaker.ObjectTree
{
	public interface IObjectDeclaration : ILocatable
	{
		IObject Object { get; }

		ProtectionLevel ProtectionLevel { get; }

		bool IsStatic { get; }
	}
}
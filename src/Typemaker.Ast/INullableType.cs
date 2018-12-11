namespace Typemaker.Ast
{
	public interface INullableType
	{
		RootType RootType { get; }

		bool IsNullable { get; }

		IObjectPath ObjectPath { get; }
	}
}
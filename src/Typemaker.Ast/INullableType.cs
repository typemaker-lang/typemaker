namespace Typemaker.Ast
{
	public interface INullableType
	{
		RootType RootType { get; }

		bool IsNullable { get; }

		string ObjectPath { get; }
	}
}
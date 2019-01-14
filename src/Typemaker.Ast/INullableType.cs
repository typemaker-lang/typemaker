namespace Typemaker.Ast
{
	public interface INullableType : ISyntaxNode
	{
		bool IsNullable { get; }
		RootType RootType { get; }

		IObjectPath ObjectPath { get; }

		INullableType IndexType { get; }

		INullableType MapType { get; }
	}
}
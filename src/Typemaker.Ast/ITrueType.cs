namespace Typemaker.Ast
{
	public interface ITrueType : ISyntaxNode
	{
		RootType RootType { get; }

		IObjectPath ObjectPath { get; }

		INullableType IndexType { get; }

		INullableType MapType { get; }
	}
}

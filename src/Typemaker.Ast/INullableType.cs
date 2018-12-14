namespace Typemaker.Ast
{
	public interface INullableType : ISyntaxNode
	{
		RootType RootType { get; }

		bool IsNullable { get; }

		IObjectPath ObjectPath { get; }
	}
}
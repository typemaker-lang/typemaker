namespace Typemaker.Ast
{
	public interface INullableType : ITrueType
	{
		bool IsNullable { get; }
	}
}
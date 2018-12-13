namespace Typemaker.Ast
{
	public interface IMapDeclaration : ISyntaxNode
	{
		string MapPath { get; }
	}
}
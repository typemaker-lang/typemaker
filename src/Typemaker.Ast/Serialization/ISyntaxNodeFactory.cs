namespace Typemaker.Ast.Serialization
{
	public interface ISyntaxNodeFactory
	{
		TSyntaxNode CreateSyntaxNode<TSyntaxNode>(SyntaxGraph graph) where TSyntaxNode : ISyntaxNode;
		ISyntaxNode CreateSyntaxNode(SyntaxGraph graph);
	}
}

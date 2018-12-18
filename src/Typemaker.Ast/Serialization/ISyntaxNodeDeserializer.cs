using System;

namespace Typemaker.Ast.Serialization
{
	interface ISyntaxNodeDeserializer
	{
		SyntaxNode CreateSyntaxNode(SyntaxGraph graph);
		NodeType GetNodeType(Type type);
	}
}
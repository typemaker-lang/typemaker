using System;
using System.Collections.Generic;
using System.Linq;

namespace Typemaker.Ast.Serialization
{
	sealed class SyntaxNodeDeserializer : ISyntaxNodeDeserializer, ISyntaxNodeFactory
	{
		readonly IReadOnlyDictionary<NodeType, Type> nodeTypeDictionary;
		readonly IReadOnlyDictionary<Type, NodeType> typeDictionary;

		static bool IsCompilationUnit(SyntaxGraph graph) => graph.NodeType == NodeType.CompilationUnit;

		public SyntaxNodeDeserializer()
		{
			nodeTypeDictionary = new Dictionary<NodeType, Type>()
			{

			};

			var typeDictionary = new Dictionary<Type, NodeType>();
			foreach (var I in nodeTypeDictionary)
				typeDictionary.Add(I.Value, I.Key);
			this.typeDictionary = typeDictionary;
		}

		public SyntaxNode CreateSyntaxNode(SyntaxGraph graph)
		{
			if (graph == null)
				throw new ArgumentNullException(nameof(graph));
			var nodeType = nodeTypeDictionary[graph.NodeType];
			return (SyntaxNode)Activator.CreateInstance(nodeType, graph, this);
		}

		public NodeType GetNodeType(Type type) => typeDictionary[type];

		public TSyntaxNode CreateSyntaxNode<TSyntaxNode>(SyntaxGraph graph) where TSyntaxNode : ISyntaxNode
		{
			if (graph == null)
				throw new ArgumentNullException(nameof(graph));
			var nodeType = nodeTypeDictionary[graph.NodeType];
			if (!typeof(TSyntaxNode).IsAssignableFrom(nodeType))
				throw new InvalidOperationException("Graph does not have required node type!");
			return (TSyntaxNode)((ISyntaxNodeFactory)this).CreateSyntaxNode(graph);
		}

		ISyntaxNode ISyntaxNodeFactory.CreateSyntaxNode(SyntaxGraph graph) => CreateSyntaxNode(graph);
	}
}

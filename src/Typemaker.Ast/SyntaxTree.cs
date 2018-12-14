using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class SyntaxTree : SyntaxNode, ISyntaxTree
	{
		public string FilePath { get; }

		public IReadOnlyList<IMapDeclaration> Maps => ChildrenAs<IMapDeclaration>();

		public IReadOnlyList<IVarDeclaration> GlobalVars => ChildrenAs<IVarDeclaration>();

		public IReadOnlyList<IEnumDefinition> Enums => ChildrenAs<IEnumDefinition>();

		public IReadOnlyList<IInterface> Interfaces => ChildrenAs<IInterface>();

		public IReadOnlyList<IProcDeclaration> ProcDeclarations => SelectChildren<IProcDeclaration>().Where(x => !(x is IProcDefinition)).ToList();

		public IReadOnlyList<IObjectDeclaration> Objects => ChildrenAs<IObjectDeclaration>();

		public IReadOnlyList<IProcDefinition> ProcDefinitions => ChildrenAs<IProcDefinition>();

		public SyntaxTree(string filePath, TypemakerParser.Compilation_unitContext context, IEnumerable<SyntaxNode> children) : base(context, children)
		{
			FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
		}

		public void Build(IList<IToken> tokens) => Build(null, null, this, this, tokens);
	}
}

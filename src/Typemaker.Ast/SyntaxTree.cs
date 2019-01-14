using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class SyntaxTree : SyntaxNode, ISyntaxTree
	{
		public string FilePath { get; private set; }

		public IEnumerable<IMapDeclaration> Maps => ChildrenAs<IMapDeclaration>();

		public IEnumerable<IVarDeclaration> GlobalVars => ChildrenAs<IVarDeclaration>();

		public IEnumerable<IEnumDefinition> Enums => ChildrenAs<IEnumDefinition>();

		public IEnumerable<IInterface> Interfaces => ChildrenAs<IInterface>();

		public IEnumerable<IProcDeclaration> ProcDeclarations => ChildrenAs<IProcDeclaration>().Where(x => !(x is IProcDefinition)).ToList();

		public IEnumerable<IObjectDeclaration> Objects => ChildrenAs<IObjectDeclaration>();

		public IEnumerable<IProcDefinition> ProcDefinitions => ChildrenAs<IProcDefinition>();

		public SyntaxTree(string filePath, TypemakerParser.Compilation_unitContext context, IEnumerable<IInternalTrivia> children) : base(context, children)
		{
			FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
		}

		public void Build(IList<IToken> tokens) => LinkTree(this, this);
	}
}

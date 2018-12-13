using Antlr4.Runtime;
using System;
using System.Collections.Generic;

namespace Typemaker.Ast
{
	sealed class SyntaxTree : SyntaxNode, ISyntaxTree
	{
		public string FilePath { get; }

		public IReadOnlyList<IMapDeclaration> Maps => ChildrenAs<IMapDeclaration>();

		public IReadOnlyList<IGlobalVarDeclaration> Globals { get; set; }

		public IReadOnlyList<IGenericDeclaration> EnumsAndInterfaces { get; set; }

		public IReadOnlyList<IGlobalProcDeclaration> Procs { get; set; }

		public IReadOnlyList<IObjectDeclaration> Datums { get; set; }

		public IReadOnlyList<IObjectProcDefinition> DatumProcs { get; set; }

		public SyntaxTree(string filePath, ParserRuleContext context, IEnumerable<SyntaxNode> children) : base(context, children)
		{
			FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
		}

		public void Build(IList<IToken> tokens) => BuildTrivia(null, null, this, tokens);
	}
}

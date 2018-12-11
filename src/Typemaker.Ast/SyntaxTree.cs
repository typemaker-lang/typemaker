using Antlr4.Runtime;
using System;
using System.Collections.Generic;

namespace Typemaker.Ast
{
	sealed class SyntaxTree : SyntaxNode, ISyntaxTree
	{
		public string FilePath { get; }

		public IReadOnlyList<IMapDeclaration> Maps => throw new NotImplementedException();

		public IReadOnlyList<IGlobalVarDeclaration> Globals => throw new NotImplementedException();

		public IReadOnlyList<IGenericDeclaration> EnumsAndInterfaces => throw new NotImplementedException();

		public IReadOnlyList<IGlobalProcDeclaration> Procs => throw new NotImplementedException();

		public IReadOnlyList<IObjectDeclaration> Datums => throw new NotImplementedException();

		public IReadOnlyList<IObjectProcDefinition> DatumProcs => throw new NotImplementedException();

		public SyntaxTree(string filePath, ParserRuleContext context) : base(context)
		{
			FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
		}

		public void Build(IList<IToken> tokens) => Build(null, null, this, tokens);
	}
}

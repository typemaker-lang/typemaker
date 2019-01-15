using System;
using System.Collections.Generic;
using System.Linq;

namespace Typemaker.Ast
{
	sealed class SyntaxTree : SyntaxNode, ISuperSyntaxTree
	{
		public string FilePath { get; }

		public IEnumerable<IMapDeclaration> Maps => ChildrenAs<IMapDeclaration>();

		public IEnumerable<IVarDeclaration> GlobalVars => ChildrenAs<IVarDeclaration>();

		public IEnumerable<IEnumDefinition> Enums => ChildrenAs<IEnumDefinition>();

		public IEnumerable<IInterface> Interfaces => ChildrenAs<IInterface>();

		public IEnumerable<IProcDeclaration> ProcDeclarations => ChildrenAs<IProcDeclaration>().Where(x => !(x is IProcDefinition));

		public IEnumerable<IObjectDeclaration> Objects => ChildrenAs<IObjectDeclaration>();

		public IEnumerable<IProcDefinition> ProcDefinitions => ChildrenAs<IProcDefinition>();

		public SyntaxTree(string filePath, IEnumerable<ITrivia> children) : base(children)
		{
			FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
			LinkTree(this, this);
		}
	}
}

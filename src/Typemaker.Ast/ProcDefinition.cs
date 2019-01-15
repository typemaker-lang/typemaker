using System.Collections.Generic;
using Typemaker.Ast.Statements;

namespace Typemaker.Ast
{
	/// <inheritdoc />
	sealed class ProcDefinition : ProcDeclaration, IProcDefinition
	{
		/// <inheritdoc />
		public IBlock Body => ChildAs<IBlock>();


		public ProcDefinition(string name, bool isVoid, bool isVerb, IObjectPath objectPath, IEnumerable<ITrivia> children) : base(name, isVoid, isVerb, objectPath, children)
		{
		}

		public ProcDefinition(IObjectPath objectPath, IEnumerable<ITrivia> children) : base(objectPath, children)
		{
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Statements;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class VarDeclaration : VarDefinition, IVarDeclaration
	{
		public IEnumerable<IDecorator> Decorators => ChildrenAs<IDecorator>();

		public VarDeclaration(string name, bool isConst, IEnumerable<ITrivia> children) : base(name, isConst, children) { }
	}
}

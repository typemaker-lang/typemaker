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

		public VarDeclaration(TypemakerParser.Var_declarationContext context, IEnumerable<IInternalTrivia> children) : base(context.var_definition_statement().var_definition_only(), children) { }
	}
}

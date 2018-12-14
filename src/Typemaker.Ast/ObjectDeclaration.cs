using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Statements;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class ObjectDeclaration : GenericDeclaration, IObjectDeclaration
	{
		public IReadOnlyList<IAssignmentStatement> VarOverrides => ChildrenAs<IAssignmentStatement>();

		public IReadOnlyList<IDecorator> Decorators => ChildrenAs<IDecorator>();

		public IReadOnlyList<ISetStatement> SetStatements => ChildrenAs<ISetStatement>();

		public IObjectPath DeclaredParentPath { get; }

		public ObjectDeclaration(TypemakerParser.Object_declarationContext context, IEnumerable<SyntaxNode> children) : base(ParseTreeFormatters.ExtractObjectPath(context.fully_extended_identifier(), false, out var baseType), context.declaration_block(), children)
		{
			DeclaredParentPath = baseType;
		}
	}
}

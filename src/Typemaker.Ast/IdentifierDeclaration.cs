using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Statements.Expressions;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	sealed class IdentifierDeclaration : SyntaxNode, IIdentifierDeclaration
	{
		public INullableType Typepath => ChildAs<INullableType>();

		public IExpression Initializer => ChildAs<IExpression>();

		public string Name { get; }

		public IdentifierDeclaration(TypemakerParser.Var_definition_statementContext context, IEnumerable<IInternalTrivia> children): base(context, children)
		{
			Name = ParseTreeFormatters.ExtractIdentifier(context.var_definition_only().typed_identifier().IDENTIFIER());
		}
	}
}

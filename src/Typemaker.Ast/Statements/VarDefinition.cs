using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Statements.Expressions;
using Typemaker.Parser;

namespace Typemaker.Ast.Statements
{
	class VarDefinition : Statement, IVarDefinition
	{
		public bool IsConst { get; }

		public string Name { get; }

		public INullableType Typepath => ChildAs<INullableType>();

		public IExpression Initializer => ChildAs<IExpression>();

		public override bool HasSideEffects => Initializer?.HasSideEffects ?? false;

		public VarDefinition(TypemakerParser.Var_definition_onlyContext context, IEnumerable<ITrivia> children) : base(context, children, false)
		{
			var typedIdentifier = context.typed_identifier();
			Name = ParseTreeFormatters.ExtractIdentifier(typedIdentifier.IDENTIFIER());
			IsConst = typedIdentifier.type()?.const_type() != null;
		}
	}
}

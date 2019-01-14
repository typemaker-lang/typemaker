using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Typemaker.Parser;

namespace Typemaker.Ast.Statements.Expressions
{
	sealed class IdentifierExpression : Expression, IIdentifierExpression
	{
		public override bool IsConstant => false;

		public override bool HasSideEffects => false;

		public IdentifierType Type { get; }

		public string Name { get; }

		public override RootType? EvaluateType() => null;

		public IdentifierExpression(TypemakerParser.Basic_identifierContext context, IEnumerable<IInternalTrivia> children) : base(context,children)
		{
			var identifier = context.IDENTIFIER();
			if (identifier != null)
			{
				Type = IdentifierType.Custom;
				Name = ParseTreeFormatters.ExtractIdentifier(identifier);
				return;
			}

			if (context.DOT() != null)
				Type = IdentifierType.ReturnValue;
			else if (context.DOTDOT() != null)
				Type = IdentifierType.ParentProc;
			else if (context.GLOBAL() != null)
				Type = IdentifierType.Src;
			else if (context.SRC() != null)
				Type = IdentifierType.Src;
			else
			{
				Debug.Assert(context.USR() != null);
				Type = IdentifierType.Usr;
			}
		}
	}
}

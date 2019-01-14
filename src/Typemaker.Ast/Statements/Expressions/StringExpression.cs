using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typemaker.Parser;

namespace Typemaker.Ast.Statements.Expressions
{
	sealed class StringExpression : Expression, IStringExpression
	{
		public override bool IsConstant => !HasFormatters;

		public bool Verbatim { get; }

		public bool HasFormatters { get; }

		public string Formatter { get; }

		public IEnumerable<IExpression> FormatExpressions => ChildrenAs<IExpression>();

		public override bool HasSideEffects => ChildrenAs<IExpression>().Any(x => x.HasSideEffects);

		public StringExpression(TypemakerParser.StringContext context, IEnumerable<ITrivia> children) : base(context, children)
		{
			var verbatimNode = context.VERBATIUM_STRING() ?? context.MULTILINE_VERBATIUM_STRING();
			Verbatim = verbatimNode != null;
			if (Verbatim)
				Formatter = ParseTreeFormatters.ExtractVerbatimString(verbatimNode);
			else
			{
				Formatter = ParseTreeFormatters.ExtractStringFormatter(context.string_body(), out var any);
				HasFormatters = any;
			}
		}

		public override RootType? EvaluateType() => RootType.String;
	}
}

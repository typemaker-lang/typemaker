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

		public bool HasFormatters => FormatExpressions.Any();

		public string Formatter { get; }

		public IEnumerable<IExpression> FormatExpressions => ChildrenAs<IExpression>();

		public override bool HasSideEffects => ChildrenAs<IExpression>().Any(x => x.HasSideEffects);

		public StringExpression(string formatter, bool verbatim, IEnumerable<ITrivia> children) : base(children)
		{
			Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
			Verbatim = verbatim;
		}

		public override RootType? EvaluateType() => RootType.String;
	}
}

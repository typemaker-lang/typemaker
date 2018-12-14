using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	public interface IStringExpression : IExpression
	{
		bool Verbatim { get; }
		bool HasFormatters { get; }
		string Formatter { get; }

		IReadOnlyList<IExpression> FormatExpressions { get; }
	}
}

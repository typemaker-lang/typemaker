using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Expressions
{
	public interface IStringExpression : IExpression
	{
		bool HasFormatters { get; }
		string Formatter { get; }
	}
}

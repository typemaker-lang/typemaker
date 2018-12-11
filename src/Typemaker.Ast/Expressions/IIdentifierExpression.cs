using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Expressions
{
	public interface IIdentifierExpression : IExpression
	{
		IdentifierType Type { get; }

		string CustomIdentifier { get; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Expressions
{
	public interface IIndexExpression : IExpression
	{
		IExpression Target { get; }
		IExpression Index { get; }
	}
}

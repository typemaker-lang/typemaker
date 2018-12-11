using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Expressions
{
	interface IListExpression : IExpression
	{
		int? InitialSize { get; }

		IReadOnlyList<IExpression> Initializer { get; }
	}
}

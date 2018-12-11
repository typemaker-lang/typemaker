using System.Collections.Generic;

namespace Typemaker.Ast.Expressions
{
	interface IDictExpression : IExpression
	{
		IReadOnlyDictionary<string, IExpression> Initializer { get; }
	}
}

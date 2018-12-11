using System.Collections.Generic;

namespace Typemaker.Ast.Expressions
{
	interface IDictExpression : IExpression
	{
		IReadOnlyList<IAssignStatement> Initializer { get; }
	}
}

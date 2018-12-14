using System.Collections.Generic;

namespace Typemaker.Ast.Statements.Expressions
{
	interface IDictExpression : IExpression
	{
		IReadOnlyList<IAssignment> Initializer { get; }
	}
}

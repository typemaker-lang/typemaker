using System.Collections.Generic;

namespace Typemaker.Ast.Statements.Expressions
{
	interface IDictExpression : IExpression
	{
		IEnumerable<IAssignment> Initializer { get; }
	}
}

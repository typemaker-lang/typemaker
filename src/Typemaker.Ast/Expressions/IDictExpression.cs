using System.Collections.Generic;
using Typemaker.Ast.Statements;

namespace Typemaker.Ast.Expressions
{
	interface IDictExpression : IExpression
	{
		IReadOnlyList<IAssignmentStatement> Initializer { get; }
	}
}

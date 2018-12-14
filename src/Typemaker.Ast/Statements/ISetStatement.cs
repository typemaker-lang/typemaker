using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast.Statements
{
	public interface ISetStatement : ISyntaxNode
	{
		bool IsAssignment { get; }

		IExpression Target { get; }
		IExpression In { get; }

		IAssignment Assignment { get; }
	}
}

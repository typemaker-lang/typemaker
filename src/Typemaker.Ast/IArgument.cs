using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast
{
	public interface IArgument : ISyntaxNode, IIdentifiable
	{
		IExpression Value { get; }
	}
}

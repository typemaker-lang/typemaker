using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Expressions;

namespace Typemaker.Ast
{
	public interface IVarDeclaration : IStatement, IIdentifiable
	{
		bool IsConst { get; }

		INullableType Type { get; }

		IExpression Initializer { get; }
	}
}

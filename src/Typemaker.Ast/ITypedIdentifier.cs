using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Expressions;

namespace Typemaker.Ast
{
	public interface ITypedIdentifier : IIdentifiable, INullableType
	{
		IExpression Initializer { get; }
	}
}

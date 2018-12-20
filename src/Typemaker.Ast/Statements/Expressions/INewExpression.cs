﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	public interface INewExpression : IExpression
	{
		IExpression AccessedType { get; }

		IReadOnlyList<IArgument> Arguments { get; }
	}
}

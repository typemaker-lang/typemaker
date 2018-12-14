using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	public interface IAccessExpression : IExpression, IIdentifiable
	{
		bool Optional { get; }
		bool Unsafe { get; }
		IExpression Target { get; }
	}
}

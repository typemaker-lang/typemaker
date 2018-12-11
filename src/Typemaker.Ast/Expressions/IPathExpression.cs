using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Expressions
{
	public interface IPathExpression : IExpression
	{
		string Path { get; }
	}
}

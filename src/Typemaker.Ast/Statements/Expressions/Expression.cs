using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typemaker.Ast.Statements.Expressions
{
	abstract class Expression : Statement, IExpression
	{
		public virtual bool IsConstant => ChildrenAs<IExpression>().All(x => x.IsConstant);

		public virtual bool IsCompileTime => IsConstant || ChildrenAs<IExpression>().All(x => x.IsCompileTime);

		public abstract RootType? EvaluateType();

		protected Expression(ParserRuleContext context, IEnumerable<ITrivia> children) : base(context, children, false) { }
	}
}

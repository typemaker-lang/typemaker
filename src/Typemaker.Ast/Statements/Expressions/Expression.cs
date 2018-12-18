using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typemaker.Ast.Serialization;

namespace Typemaker.Ast.Statements.Expressions
{
	abstract class Expression : Statement, IExpression
	{
		public virtual bool IsConstant => ChildrenAs<IExpression>().All(x => x.IsConstant);

		public abstract RootType? EvaluateType();

		protected Expression(ParserRuleContext context, IEnumerable<SyntaxNode> children) : base(context, children, false) { }
	}
}

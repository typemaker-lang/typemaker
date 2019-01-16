using System.Collections.Generic;

namespace Typemaker.Ast.Statements.Expressions
{
	sealed class BracketedExpression : Expression, IBracketedExpression
	{
		public IExpression Interior => ChildAs<IExpression>();

		public override bool HasSideEffects => Interior.HasSideEffects;

		public override RootType? EvaluateType() => Interior.EvaluateType();

		public BracketedExpression(IEnumerable<ITrivia> children) : base(children)
		{

		}
	}
}

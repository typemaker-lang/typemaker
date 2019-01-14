using System.Collections.Generic;
using Typemaker.Ast.Statements.Expressions;
using Typemaker.Parser;

namespace Typemaker.Ast.Statements
{
	sealed class JumpStatement : Statement, IJumpStatement
	{
		public override bool HasSideEffects => false;

		public JumpType JumpType { get; }

		public IExpression Throw => ChildAs<IExpression>();
		
		public JumpStatement(TypemakerParser.Jump_statementContext context, IEnumerable<IInternalTrivia> children) : base(context, children, true)
		{
			if (context.BREAK() != null)
				JumpType = JumpType.Break;
			else if (context.CONTINUE() != null)
				JumpType = JumpType.Continue;
			else
				JumpType = JumpType.Throw;
		}
	}
}

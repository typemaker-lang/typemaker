using System.Collections.Generic;
using Typemaker.Ast.Statements;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	/// <inheritdoc />
	sealed class ProcDefinition : ProcDeclaration, IProcDefinition
	{
		/// <inheritdoc />
		public IStatement Body => ChildAs<IStatement>();

		/// <summary>
		/// Construct a <see cref="ProcDefinition"/>
		/// </summary>
		/// <param name="context">The <see cref="TypemakerParser.Proc_definitionContext"/></param>
		/// <param name="chidren">The child <see cref="ITrivia"/>s</param>
		public ProcDefinition(TypemakerParser.Proc_definitionContext context, IEnumerable<ITrivia> chidren) : base(context.proc(), chidren)
		{ }
	}
}

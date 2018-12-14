using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using Typemaker.Ast.Statements;
using Typemaker.Parser;

namespace Typemaker.Ast.Visitors
{
	sealed class BlockVisitor : TypemakerVisitor
	{
		readonly ITypemakerVisitor expressionVisitor;

		public BlockVisitor(ITypemakerVisitor expressionVisitor)
		{
			this.expressionVisitor = expressionVisitor ?? throw new ArgumentNullException(nameof(expressionVisitor));
		}
	}
}
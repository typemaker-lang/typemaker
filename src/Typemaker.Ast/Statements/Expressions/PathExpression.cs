using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Parser;

namespace Typemaker.Ast.Statements.Expressions
{
	sealed class PathExpression : Expression, IPathExpression
	{
		public override bool IsConstant => true;

		public override bool HasSideEffects => false;

		public IObjectPath ObjectPath { get; }

		public override RootType? EvaluateType() => RootType.Path;

		public PathExpression(TypemakerParser.Fully_extended_identifierContext context, IEnumerable<ITrivia> children) : base(children)
		{
			ParseTreeFormatters.ExtractObjectPath(context, true, out var objectPath);
			ObjectPath = objectPath;
		}
	}
}

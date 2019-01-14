using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Statements.Expressions;
using Typemaker.Parser;

namespace Typemaker.Ast.Statements
{
	class VarDefinition : Statement, IVarDefinition
	{
		public bool IsConst { get; }

		public string Name { get; }

		public INullableType Typepath => ChildAs<INullableType>();

		public IExpression Initializer => ChildAs<IExpression>();

		public override bool HasSideEffects => Initializer?.HasSideEffects ?? false;

		public VarDefinition(string name, bool isConst, IEnumerable<ITrivia> children) : base(children, false)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			IsConst = isConst;
		}
	}
}

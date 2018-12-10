using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	interface IObjectDeclaration : ISyntaxNode, IGlobalDeclaration, IIdentifiable
	{
		bool IsPartial { get; }

		bool IsAbstract { get; }

		bool IsSealed { get; }
	}
}

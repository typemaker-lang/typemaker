using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	interface ITypedIdentifier : ISyntaxNode, IIdentifiable
	{
		RootType RootType { get; }

		bool IsNullable { get; }
	}
}

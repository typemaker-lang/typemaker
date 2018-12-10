using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	interface IProcDeclaration : ISyntaxNode, IIdentifiable
	{
		Protection ProtectionLevel { get; }
		bool IsStatic { get; }
		bool IsVirtual { get; }

		bool IsVerb { get; }
	}
}

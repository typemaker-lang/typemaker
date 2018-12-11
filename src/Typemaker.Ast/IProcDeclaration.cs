using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IProcDeclaration : ISyntaxNode, IIdentifiable
	{
		Protection ProtectionLevel { get; }
		bool IsStatic { get; }
		bool IsVirtual { get; }
		bool IsVerb { get; }

		bool IsVoid { get; }

		INullableType ReturnType { get; }
	}
}

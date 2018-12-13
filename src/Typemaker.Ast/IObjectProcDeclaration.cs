using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IObjectProcDeclaration : IProcDeclaration
	{
		bool IsStatic { get; }
		bool IsVirtual { get; }

		Protection ProtectionLevel { get; }

		new IObjectProcDefinition Definition { get; }
	}
}

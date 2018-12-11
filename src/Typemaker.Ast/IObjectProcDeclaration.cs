using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IObjectProcDeclaration : IProcDeclaration
	{
		Protection ProtectionLevel { get; }
		bool IsStatic { get; }
		bool IsVirtual { get; }
	}
}

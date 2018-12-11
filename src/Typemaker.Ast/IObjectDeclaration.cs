using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IObjectDeclaration : IGlobalDeclaration, IInterfaceImplementer
	{
		bool IsPartial { get; }

		bool IsAbstract { get; }

		bool IsSealed { get; }
	}
}

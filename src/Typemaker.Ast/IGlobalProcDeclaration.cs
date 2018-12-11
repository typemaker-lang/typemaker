using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public interface IGlobalProcDeclaration : IGlobalDeclaration, IIdentifiable
	{
		IProcDefinition ProcDefinition { get; }
	}
}

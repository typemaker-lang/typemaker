using System.Collections.Generic;

namespace Typemaker.Ast
{
	public interface IInterfaceImplementer : IIdentifiable
	{
		IReadOnlyList<IImplements> Implements { get; }
	}
}
using System.Collections.Generic;

namespace Typemaker.Ast
{
	public interface IInterfaceImplementer : ISyntaxNode, IIdentifiable
	{
		IReadOnlyList<IImplementsStatement> Implements { get; }
	}
}
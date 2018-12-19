using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	public interface ICodeTree
	{
		IObject TypemakerObject { get; }

		IReadOnlyList<IVariableDeclaration> GlobalVariables { get; }

		IReadOnlyList<IProcDefinition> GlobalProcs { get; }

		IReadOnlyList<IEnum> Enums { get; }

		IReadOnlyList<IInterface> Interfaces { get; }

		IReadOnlyList<IMapDeclaration> Maps { get; }
	}
}

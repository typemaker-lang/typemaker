using System;
using System.Collections.Generic;

namespace Typemaker.ObjectTree
{
	sealed class ObjectTree : IObjectTree
	{
		public IObject RootObject { get; }

		public IReadOnlyList<IGlobalVariableDeclaration> Variables => throw new NotImplementedException();

		public IReadOnlyList<IProcDeclaration> Procs => throw new NotImplementedException();

		public IReadOnlyList<IEnum> Enums => throw new NotImplementedException();

		public IReadOnlyList<IInterface> Interfaces => throw new NotImplementedException();

		public IObject LookupPath(string extendedIdentifier)
		{
			throw new NotImplementedException();
		}
	}
}

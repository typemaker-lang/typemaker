using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.ObjectTree
{
	class ProcDefinition : IProcDefinition
	{
		public bool IsInline { get; }

		public IProcDeclaration Declaration { get; }

		public Highlight Location { get; }

		public ProcDefinition(IProcDeclaration declaration, Highlight location, bool inline)
		{
			Declaration = declaration ?? throw new ArgumentNullException(nameof(declaration));
			Location = location ?? throw new ArgumentNullException(nameof(location));
			IsInline = inline;
		}
	}
}

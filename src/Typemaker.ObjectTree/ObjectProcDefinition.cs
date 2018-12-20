using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.ObjectTree
{
	class ObjectProcDefinition : ProcDefinition, IObjectProcDefinition
	{
		public bool IsFinal { get; }

		public int Precedence { get; }

		public IObjectProcDefinition Parent { get; }

		public new IObjectProcDeclaration Declaration { get; }

		public ObjectProcDefinition(IObjectProcDeclaration declaration, IObjectProcDefinition parent, Highlight location, int precedence, bool final, bool inline) : base(declaration, location, inline)
		{
			Declaration = declaration;
			Parent = parent;
			Precedence = precedence;
			IsFinal = final;
		}

		public IObjectProcDefinition FixParentChainAfterFileRemoval(string filePath)
		{
			throw new NotImplementedException();
		}
	}
}

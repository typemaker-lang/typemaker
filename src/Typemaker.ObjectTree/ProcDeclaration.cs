using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	class ProcDeclaration : IProcDeclaration
	{
		public string Name { get; }

		public bool IsVerb { get; }

		public ITypeDeclaration ReturnType { get; }

		public IReadOnlyList<IArgumentDeclaration> Arguments { get; }

		public ProcDeclaration(string name, ITypeDeclaration returnType, IEnumerable<IArgumentDeclaration> arguments, bool isVerb)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
			Arguments = arguments?.ToList() ?? throw new ArgumentNullException(nameof(arguments)); // yo dawg
			IsVerb = isVerb;
		}
	}
}

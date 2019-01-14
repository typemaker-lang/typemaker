using System;
using System.Collections.Generic;
using System.Linq;

namespace Typemaker.Ast
{
	class ProcDeclaration : SyntaxNode, IProcDeclaration
	{
		public string Name { get; }

		public bool IsVerb { get; }

		public bool IsConstructor { get; }

		public IObjectPath ObjectPath { get; }

		public IEnumerable<IArgumentDeclaration> Arguments => ChildrenAs<IArgumentDeclaration>();

		public INullableType ReturnType => isVoid ? null : ChildrenAs<INullableType>().Last();

		public IEnumerable<IDecorator> Decorators => ChildrenAs<IDecorator>();

		readonly bool isVoid;

		protected ProcDeclaration(string name, bool isVoid, bool isVerb, IEnumerable<ITrivia> children) : base(children)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			this.isVoid = isVoid;
			IsVerb = isVerb;
		}

		public ProcDeclaration(string name, bool isVoid, bool isVerb, IObjectPath objectPath, IEnumerable<ITrivia> children) : this(name, isVoid, isVerb, children)
		{
			ObjectPath = objectPath;
		}

		public ProcDeclaration(IObjectPath objectPath, IEnumerable<ITrivia> children) : base(children)
		{
			ObjectPath = objectPath ?? throw new ArgumentNullException(nameof(objectPath));
			Name = "New";
		}
	}
}

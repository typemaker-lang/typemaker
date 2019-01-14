using Antlr4.Runtime;
using System.Collections.Generic;
using System.Linq;
using Typemaker.Parser;

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

		protected ProcDeclaration(TypemakerParser.ProcContext context, IEnumerable<ITrivia> children) : base(children)
		{
			isVoid = context.proc_return_declaration()?.return_type().VOID() != null;
			IsVerb = context.proc_type().VERB() != null;

			var fEI = context.fully_extended_identifier();
			if(fEI != null)
			{
				ParseTreeFormatters.ExtractObjectPath(fEI, true, out var objectPath);
				ObjectPath = objectPath;
			}

			var idOrCon = context.identifier_or_constructor();
			var identifier = idOrCon.IDENTIFIER();
			IsConstructor = identifier == null;
			if (IsConstructor)
				Name = TypemakerLexer.DefaultVocabulary.GetLiteralName(TypemakerLexer.CONSTRUCTOR);
			else
				Name = ParseTreeFormatters.ExtractIdentifier(identifier);
		}

		public ProcDeclaration(TypemakerParser.Proc_declarationContext context, IEnumerable<ITrivia> children) : this(context.proc(), children)
		{ }
	}
}

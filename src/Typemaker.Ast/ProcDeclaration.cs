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

		public IReadOnlyList<IIdentifierDeclaration> Arguments => ChildrenAs<IIdentifierDeclaration>();

		public INullableType ReturnType => isVoid ? null : SelectChildren<INullableType>().Last();

		public IReadOnlyList<IDecorator> Decorators => ChildrenAs<IDecorator>();

		readonly bool isVoid;

		protected ProcDeclaration(TypemakerParser.ProcContext context, IEnumerable<SyntaxNode> children) : base(context, children)
		{
			isVoid = context.proc_return_declaration()?.return_type().VOID() != null;
			IsVerb = context.proc_type().VERB() != null;
			var identifier = context.identifier_or_constructor().IDENTIFIER();
			IsConstructor = identifier == null;
			if (IsConstructor)
				Name = TypemakerLexer.DefaultVocabulary.GetLiteralName(TypemakerLexer.CONSTRUCTOR);
			else
				Name = ParseTreeFormatters.ExtractIdentifier(identifier);
		}

		public ProcDeclaration(TypemakerParser.Proc_declarationContext context, IEnumerable<SyntaxNode> children) : this(context.proc(), children)
		{ }
	}
}

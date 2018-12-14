using System.Collections.Generic;
using System.Linq;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	abstract class ProcDeclaration : SyntaxNode, IProcDeclaration
	{
		public string Name { get; }

		public bool IsVerb { get; }

		public bool IsConstructor { get; }

		public IReadOnlyList<ITypedIdentifierDeclaration> Arguments => ChildrenAs<ITypedIdentifierDeclaration>();

		public INullableType ReturnType => isVoid ? null : SelectChildren<INullableType>().Last();

		public IProcDefinition Definition => ChildAs<IProcDefinition>();

		public IReadOnlyList<IDecorator> Decorators => ChildrenAs<IDecorator>();

		readonly bool isVoid;

		public ProcDeclaration(TypemakerParser.ProcContext context, IEnumerable<SyntaxNode> children) : base(context, children)
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
	}
}

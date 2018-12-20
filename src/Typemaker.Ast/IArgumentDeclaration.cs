using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast
{
	public interface IArgumentDeclaration : ISyntaxNode
	{
		INullableType VariadicType { get; }

		IIdentifierDeclaration IdentifierDeclaration { get; }

		IExpression Initializer { get; }
	}
}
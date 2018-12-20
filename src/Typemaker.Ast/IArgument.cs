using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast
{
	public interface IArgument : ISyntaxNode
	{
		INullableType VariadicType { get; }

		IIdentifierDeclaration IdentifierDeclaration { get; }

		IExpression Initializer { get; }
	}
}
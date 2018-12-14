using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast
{
	public interface ITypedIdentifierDeclaration : IIdentifiable
	{
		INullableType Typepath { get; }
		IExpression Initializer { get; }
	}
}

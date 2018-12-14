using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast
{
	public interface IIdentifierDeclaration : IIdentifiable
	{
		INullableType Typepath { get; }
		IExpression Initializer { get; }
	}
}

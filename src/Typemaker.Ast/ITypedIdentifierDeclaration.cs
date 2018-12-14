using Typemaker.Ast.Expressions;

namespace Typemaker.Ast
{
	public interface ITypedIdentifierDeclaration : IIdentifiable, INullableType
	{
		IExpression Initializer { get; }
	}
}

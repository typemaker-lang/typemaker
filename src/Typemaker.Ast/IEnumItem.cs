using Typemaker.Ast.Expressions;

namespace Typemaker.Ast
{
	public interface IEnumItem : ISyntaxNode, IIdentifiable
	{
		bool AutoValue { get; }

		IStringExpression StringExpression { get; }

		IIntegerExpression IntegerExpression { get; }
	}
}
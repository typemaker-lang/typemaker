using Typemaker.Ast.Expressions;

namespace Typemaker.Ast
{
	public interface IEnumItem : ISyntaxNode, IIdentifiable
	{
		bool AutoValue { get; }

		IExpression Expression { get; }
	}
}
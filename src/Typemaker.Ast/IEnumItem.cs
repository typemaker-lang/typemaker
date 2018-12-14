using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast
{
	public interface IEnumItem : ISyntaxNode, IIdentifiable
	{
		bool AutoValue { get; }

		IExpression Expression { get; }
	}
}
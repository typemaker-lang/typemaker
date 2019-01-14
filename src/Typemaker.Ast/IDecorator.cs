using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast
{
	public interface IDecorator : ISyntaxNode
	{
		DecoratorType Type { get; }

		IIntegerExpression Precedence { get; }

		bool? PublicProtection { get; }
	}
}
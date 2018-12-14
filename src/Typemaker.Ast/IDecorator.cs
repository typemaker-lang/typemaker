namespace Typemaker.Ast
{
	public interface IDecorator : ISyntaxNode
	{
		DecoratorType Type { get; }

		long? Precedence { get; }

		bool? PublicProtection { get; }
	}
}
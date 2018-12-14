namespace Typemaker.Ast
{
	public interface IDecorator : ISyntaxNode
	{
		Decorator Type { get; }

		int? Precedence { get; }

		bool? PublicProtection { get; }
	}
}
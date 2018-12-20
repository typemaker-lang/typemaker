namespace Typemaker.Ast.Validation
{
	public sealed class ValidationError
	{
		public ValidationErrorCode Code { get; set; }
		public string Description { get; set; }
		public ILocatable Location { get; set; }
	}
}
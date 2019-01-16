namespace Typemaker.Ast.Validation
{
	public enum ValidationErrorCode
	{
		UnorderedDecorator,
		InvalidDecorator,
		IncompatibleDecorator,
		MissingDecoratorOrBody,
		InvalidEnumExpression,
		NonConstEnumStringItem,
		DeclaredProcUndeclaredObject,
		InvalidDeclarationSetTarget,
		InvalidDeclarationSetExpression,
		NonConstDeclarationSetExpression,
		DeclarationSetUndeclaredObject,
		SetStatementInInterface,
		NestedUnsafeBlock,
		InvalidTriviaLocation,
		InvalidDeclarationSetType,
		InvalidDeclarationSetTargetExpression,
		IncompatibleYield
	}
}
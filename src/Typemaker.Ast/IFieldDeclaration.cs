namespace Typemaker.Ast
{
	public interface IFieldDeclaration : ISyntaxNode
	{
		Protection ProtectionLevel { get; }
		bool IsReadonly { get; }
		bool IsStatic { get; }
		IVarDeclaration VarDeclaration { get; }
	}
}

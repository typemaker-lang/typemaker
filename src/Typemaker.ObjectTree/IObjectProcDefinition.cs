namespace Typemaker.CodeTree
{
	public interface IObjectProcDefinition : IProcDefinition
	{
		bool IsFinal { get; }

		int Precedence { get; }

		new IObjectProcDeclaration Declaration { get; }

		IObjectProcDefinition Parent { get; }
	}
}

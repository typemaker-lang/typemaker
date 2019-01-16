using Typemaker.Ast;
using Typemaker.Ast.Validation;

namespace Typemaker.ObjectTree
{
	public interface IObjectTreeFactory
	{
		IObjectTree CreateObjectTree();
		void AddOrUpdateAst(IObjectTree objectTree, IValidSyntaxTree validSyntaxTree, IExpressionReducer expressionReducer);
	}
}
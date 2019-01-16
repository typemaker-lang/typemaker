using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typemaker.Ast;
using Typemaker.Ast.Statements.Expressions;
using Typemaker.Ast.Validation;

namespace Typemaker.ObjectTree
{
	public sealed class ObjectTreeFactory : IObjectTreeFactory
	{

		public IObjectTree CreateObjectTree() => new ObjectTree(new BaseObject());

		public void AddOrUpdateAst(IObjectTree objectTree, IValidSyntaxTree validSyntaxTree, IExpressionReducer expressionReducer)
		{
			if (objectTree == null)
				throw new ArgumentNullException(nameof(objectTree));
			if (validSyntaxTree == null)
				throw new ArgumentNullException(nameof(validSyntaxTree));
			if (expressionReducer == null)
				throw new ArgumentNullException(nameof(expressionReducer));

			var syntaxTree = validSyntaxTree.SyntaxTree;
			var filePath = syntaxTree.FilePath;
			objectTree.RemoveFileItems(filePath);

			Highlight CreateLocation(Ast.ILocatable locatable) => new Highlight
			{
				Start = locatable.Start,
				End = locatable.End,
				FilePath = filePath
			};

			foreach (var I in syntaxTree.Enums)
				objectTree.AddEnum(new EnumDeclaration(I.Name, CreateLocation(I), I.Items.Select(x =>
				{
					var location = CreateLocation(x);
					if (x.Expression == null)
						return new EnumItem(null, x.Name, location);

					var stringResult = expressionReducer.Reduce<string>(x.Expression);
					if (stringResult != null)
						return new EnumItem(stringResult, x.Name, location);

					var intResult = expressionReducer.Reduce<int>(x.Expression);
					return new EnumItem(intResult, x.Name, location);
				})));

		}
	}
}

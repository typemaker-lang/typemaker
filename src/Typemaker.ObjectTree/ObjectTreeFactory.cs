using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typemaker.Ast;
using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.ObjectTree
{
	public static class ObjectTreeFactory
	{
		public static IObjectTree CreateObjectTree() => new ObjectTree(new BaseObject());

		public static void AddOrUpdateAst(IObjectTree objectTree, ISyntaxTree syntaxTree, IExpressionReducer expressionReducer)
		{
			if (objectTree == null)
				throw new ArgumentNullException(nameof(objectTree));
			if (syntaxTree == null)
				throw new ArgumentNullException(nameof(syntaxTree));
			if (expressionReducer == null)
				throw new ArgumentNullException(nameof(expressionReducer));

			var filePath = syntaxTree.FilePath;
			objectTree.RemoveFileItems(filePath);

			Location CreateLocation(Ast.ILocatable locatable) => new Location
			{
				Start = locatable.Start,
				End = locatable.End,
				FilePath = filePath
			};

			void UnvalidatedAst() => throw new InvalidOperationException("syntaxTree not validated!");

			foreach (var I in syntaxTree.Enums)
				objectTree.AddEnum(new EnumDeclaration(I.Name, CreateLocation(I), I.Items.Select(x =>
				{
					var location = CreateLocation(x);
					if (x.Expression == null)
						return new EnumItem(null, x.Name, location);

					var stringResult = expressionReducer.ReduceR<string>(x.Expression);
					if (stringResult != null)
						return new EnumItem(stringResult, x.Name, location);

					var intResult = expressionReducer.ReduceS<int>(x.Expression);
					if (!intResult.HasValue)
						UnvalidatedAst();
					return new EnumItem(intResult.Value, x.Name, location);
				})));

		}
	}
}

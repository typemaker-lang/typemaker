using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Typemaker.Ast.Statements;
using Typemaker.Ast.Statements.Expressions;

namespace Typemaker.Ast.Validation
{
	public sealed class SyntaxTreeValidator : ISyntaxTreeValidator
	{
		/*
		 * So the CFG generated parser could, in theory, validate all this stuff
		 * But Cyberboss has smooth brain syndrome and doesn't know how to translate the errors into understandable ones
		 * So we do some validation ourselves
		 * 
		 * Full list:
		 * Decorator types, ordering, and duplication
		 * Set assignment statements in interfaces and objects (banned in intefaces, only allowed in object declarations, and only a certain list of valid identifiers with const expressions)
		 * Enum assignments are const strings or ints
		 * Nested unsafe blocks
		 * 
		 */

		readonly DecoratorType[] decoratorOrder = new DecoratorType[] {
			DecoratorType.Declare,
			DecoratorType.Protection,
			DecoratorType.Readonly,
			DecoratorType.Sealed,
			DecoratorType.Abstract,
			DecoratorType.Virtual,
			DecoratorType.Final,
			DecoratorType.Partial,
			DecoratorType.Explicit,
			DecoratorType.Inline,
			DecoratorType.Precedence
		};
		readonly IReadOnlyDictionary<DecoratorType, DecoratorType[]> incompatibleDecorators = new Dictionary<DecoratorType, DecoratorType[]> {
			{ DecoratorType.Explicit, new DecoratorType[] { DecoratorType.Declare } },
			{ DecoratorType.Abstract, new DecoratorType[] { DecoratorType.Sealed, DecoratorType.Virtual, DecoratorType.Final, DecoratorType.Inline } },
			{ DecoratorType.Virtual, new DecoratorType[] { DecoratorType.Final, DecoratorType.Inline } },
			{ DecoratorType.Final,new DecoratorType[] { DecoratorType.Inline } }
		};

		IEnumerable<ValidationError> CheckDecorators(string owningName, IDecorated decorated, params DecoratorType[] allowedDecorators)
		{
			var index = -1;
			var seenDecoratorPositions = new Dictionary<DecoratorType, int>();
			string FormatDecorator(DecoratorType type) => type.ToString().ToLower();
			foreach (var decorator in decorated.Decorators)
			{
				var decoratorType = decorator.Type;

				if (allowedDecorators.Any(x => x == decoratorType))
				{
					var useAn = decoratorType == DecoratorType.Abstract || decoratorType == DecoratorType.Explicit || decoratorType == DecoratorType.Inline;
					yield return new ValidationError
					{
						Code = ValidationErrorCode.InvalidDecorator,
						Description = String.Format(CultureInfo.InvariantCulture, "{0}s cannot have a{1} {2} decorator", owningName, useAn ? "n" : String.Empty, FormatDecorator(decoratorType)),
						Location = decorator
					};
					continue;
				}

				var decoratorTypeIndex = Array.IndexOf(decoratorOrder, decoratorType);

				ValidationError Duplicated() => new ValidationError
				{
					Code = ValidationErrorCode.UnorderedDecorator,
					Description = "Duplicated decorator",
					Location = decorator
				};

				if (decoratorTypeIndex == index)
				{
					yield return Duplicated();
					continue;
				}

				++index;

				if (decoratorTypeIndex < index)
				{
					foreach (var I in seenDecoratorPositions.Reverse())
						if (I.Key == decoratorType)
							yield return Duplicated();
						else if (I.Value > decoratorTypeIndex)
						{
							yield return new ValidationError
							{
								Code = ValidationErrorCode.UnorderedDecorator,
								Description = String.Format(CultureInfo.InvariantCulture, "{0} must come before {1}", FormatDecorator(decoratorType), FormatDecorator(I.Key)),
								Location = decorator
							};
							break;
						}
					index = decoratorTypeIndex;
				}
				seenDecoratorPositions.Add(decoratorType, decoratorTypeIndex);
			}

			foreach (var I in seenDecoratorPositions)
			{
				DecoratorType incompatibleType = default;
				if (incompatibleDecorators.TryGetValue(I.Key, out var incomaptibleTypes) && incomaptibleTypes.Any(x =>
				{
					if (seenDecoratorPositions.ContainsKey(x))
					{
						incompatibleType = x;
						return true;
					}
					return false;
				}))
					yield return new ValidationError
					{
						Code = ValidationErrorCode.IncompatibleDecorator,
						Description = String.Format(CultureInfo.InvariantCulture, "{0} is incompatible with {1}", FormatDecorator(I.Key), FormatDecorator(incompatibleType)),
						Location = decorated
					};
			}
		}

		IEnumerable<ValidationError> ValidateEnums(ISyntaxTree syntaxTree)
		{
			foreach (var I in syntaxTree.Enums)
			{

				foreach (var J in I.Items)
				{
					var expression = J.Expression;
					if (expression == null)
						continue;

					var rootType = expression.EvaluateType();
					if (rootType == RootType.String && !expression.IsConstant)
						yield return new ValidationError
						{
							Code = ValidationErrorCode.NonConstEnumStringItem,
							Description = "Enum string items must be constant",
							Location = J
						};
					else if (rootType != RootType.Integer)
						yield return new ValidationError
						{
							Code = ValidationErrorCode.InvalidEnumExpression,
							Description = "Enum items may only be integers or constant strings",
							Location = J
						};
				}
			}
		}

		void ValidateInterfaces(ISyntaxTree syntaxTree, List<ValidationError> errors)
		{
			foreach(var I in syntaxTree.Interfaces)
			{
				foreach (var J in I.ChildrenAs<ISetStatement>())
					errors.Add(new ValidationError
					{
						Code = ValidationErrorCode.SetStatementInInterface,
						Description = "Interfaces cannot have set statements",
						Location = J
					});
			}
		}

		void ValidateObjects(ISyntaxTree syntaxTree, List<ValidationError> errors)
		{
			foreach (var I in syntaxTree.Objects)
			{
				var declared = I.Decorators.Any(x => x.Type == DecoratorType.Declare);

				errors.AddRange(CheckDecorators("Object", I, DecoratorType.Declare, DecoratorType.Abstract, DecoratorType.Explicit, DecoratorType.Inline, DecoratorType.Partial, DecoratorType.Sealed));

				foreach (var J in I.VarDeclarations)
					errors.AddRange(CheckDecorators("Field", J, DecoratorType.Explicit, DecoratorType.Protection, DecoratorType.Readonly));

				var anyProcs = false;
				foreach (var J in I.ProcDeclarations)
				{
					errors.AddRange(CheckDecorators("Proc declaration", J, DecoratorType.Protection, DecoratorType.Final, DecoratorType.Inline));
					anyProcs = true;
				}
				if (anyProcs && !declared)
					errors.Add(new ValidationError
					{
						Code = ValidationErrorCode.DeclaredProcUndeclaredObject,
						Description = "An object that is not 'declare'd cannot contain proc declarations",
						Location = I
					});

				var badSets = false;
				foreach (var J in I.SetStatements)
				{
					if (declared)
					{
						if(!J.IsAssignment)
						{
							errors.Add(new ValidationError
							{
								Code = ValidationErrorCode.InvalidDeclarationSetType,
								Description = "A set in statement is not valid here",
								Location = J
							});
							continue;
						}

						var assignment = J.Assignment;
						if(!(assignment.LeftHandSide is IIdentifierExpression identifierExpression))
						{
							errors.Add(new ValidationError
							{
								Code = ValidationErrorCode.InvalidDeclarationSetTargetExpression,
								Description = "Only an identifier may be used here",
								Location = J
							});
							continue;
						}

						var identifier = identifierExpression.Name;
						var rhs = assignment.RightHandSide;
						var expressionRootType = rhs.EvaluateType();
						if (!DeclarationSetTargets.Map.TryGetValue(identifier, out var targetRootType))
							errors.Add(new ValidationError
							{
								Code = ValidationErrorCode.InvalidDeclarationSetTarget,
								Description = "Invalid set statement for declaration",
								Location = J
							});
						else if (targetRootType != expressionRootType)
							errors.Add(new ValidationError
							{
								Code = ValidationErrorCode.InvalidDeclarationSetExpression,
								Description = String.Format(CultureInfo.InvariantCulture, "{0} must be a {1} value", identifier, expressionRootType.ToString().ToLower()),
								Location = J
							});
						else if(!rhs.IsConstant)
							errors.Add(new ValidationError
							{
								Code = ValidationErrorCode.NonConstDeclarationSetExpression,
								Description = "Declaration set statements must be constant",
								Location = J
							});
					}
					else
					{
						badSets = true;
						break;
					}
				}
				if (badSets)
					errors.Add(new ValidationError
					{
						Code = ValidationErrorCode.DeclarationSetUndeclaredObject,
						Description = "An object that is not 'declare'd cannot contain set statements",
						Location = I
					});
			}
		}
		void CheckBlock(IStatement body, List<ValidationError> errors, bool nested = false)
		{
			if (body is IBlock block)
				if (nested && block.Unsafe)
					errors.Add(new ValidationError
					{
						Code = ValidationErrorCode.NestedUnsafeBlock,
						Description = "An unsafe block cannot appear inside another unsafe block",
						Location = block
					});
				else
					foreach (var I in block.Statements)
						CheckBlock(I, errors, nested || block.Unsafe);
			else if (body is ISwitchStatement switchStatement)
			{
				foreach (var I in switchStatement.Cases)
					CheckBlock(I.Body, errors, nested);
				if(switchStatement.Default != null)
					CheckBlock(switchStatement.Default, errors, nested);
			}
			else if (body is IIfStatement ifStatement)
			{
				foreach (var I in ifStatement.ElseIfs)
					CheckBlock(I.Body, errors, nested);
				CheckBlock(ifStatement.Body, errors, nested);
				if(ifStatement.Else != null)
					CheckBlock(ifStatement.Else, errors, nested);
			}
			else if (body is IBodiedStatement bodiedStatement)
				CheckBlock(bodiedStatement.Body, errors, nested);
		}

		public SyntaxTreeValidationResult ValidateSyntaxTree(ISyntaxTree syntaxTree)
		{
			if (syntaxTree == null)
				throw new ArgumentNullException(nameof(syntaxTree));

			var errors = new List<ValidationError>();

			foreach (var I in syntaxTree.GlobalVars)
				errors.AddRange(CheckDecorators("Global variable", I, DecoratorType.Declare, DecoratorType.Explicit));

			errors.AddRange(ValidateEnums(syntaxTree));

			ValidateInterfaces(syntaxTree, errors);

			ValidateObjects(syntaxTree, errors);
			
			foreach (var I in syntaxTree.ProcDeclarations)
			{
				errors.AddRange(CheckDecorators("proc declaration", I, DecoratorType.Declare, DecoratorType.Inline, DecoratorType.Protection));
				if (!I.Decorators.Any(x => x.Type == DecoratorType.Declare || x.Type == DecoratorType.Abstract))
					errors.Add(new ValidationError
					{
						Code = ValidationErrorCode.MissingDecoratorOrBody,
						Description = "A proc declaration must be 'abstract' or 'declare'd",
						Location = I
					});
			}

			foreach(var I in syntaxTree.ProcDefinitions)
			{
				errors.AddRange(CheckDecorators("proc definition", I, DecoratorType.Explicit, DecoratorType.Final, DecoratorType.Virtual, DecoratorType.Inline, DecoratorType.Precedence, DecoratorType.Protection));

				CheckBlock(I.Body, errors);
			}

			/*
			IEnumerable<ISyntaxNode> WalkTree(ISyntaxNode syntaxNode)
			{
				yield return syntaxNode;
				foreach (var I in syntaxNode.Children)
					foreach (var J in WalkTree(I))
						yield return J;
			}
			*/
			
			return new SyntaxTreeValidationResult
			{
				Errors = errors,
				SyntaxTree = errors.Count == 0 ? new ValidSyntaxTree(syntaxTree) : null
			};
		}

	}
}

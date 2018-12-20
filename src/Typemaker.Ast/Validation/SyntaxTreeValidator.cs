using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
		 * Set assignment statements in interfaces and objects (banned in intefaces, only allowed in object declarations, and only a certain list of valid identifiers)
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

		void ValidateInterfaces(ISyntaxTree syntaxTree, List<ValidationError> errors)
		{
			throw new NotImplementedException();
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
						var assignment = J.Assignment;
						var identifier = ((IIdentifierExpression)assignment.LeftHandSide).CustomIdentifier;
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
			}
			
			return new SyntaxTreeValidationResult
			{
				Errors = errors,
				SyntaxTree = errors.Count == 0 ? new ValidSyntaxTree(syntaxTree) : null
			};
		}
	}
}

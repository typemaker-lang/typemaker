using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Typemaker.Ast.Tests
{
	sealed class SyntaxNodeValidator<TValidateType>: ISyntaxNodeValidator where TValidateType : ISyntaxNode
	{
		public bool ValidateChildren { get; set; } = true;

		public List<ISyntaxNodeValidator> Children { get; set; } = new List<ISyntaxNodeValidator>();

		readonly Func<TValidateType, bool> validator;

		readonly Type type;

		public SyntaxNodeValidator(Func<TValidateType, bool> validator = null)
		{
			type = typeof(TValidateType);
			this.validator = validator;
		}

		public void Validate(ISyntaxNode syntaxNode, bool validateTrivia)
		{
			if (syntaxNode == null)
				throw new ArgumentNullException(nameof(syntaxNode));

			if (!ValidateChildren && Children.Count > 0)
				throw new InvalidOperationException("Children not empty but ValidateChildren is false!");

			Assert.True(syntaxNode is TValidateType, $"Expected {type.Name} got {syntaxNode.GetType().Name}!");

			if(validator != null)
				Assert.True(validator((TValidateType)syntaxNode), $"Validation failed for {type.Name}!");

			IReadOnlyList <ISyntaxNode> childrenSelect;
			if (validateTrivia)
				childrenSelect = syntaxNode.Children;
			else
				childrenSelect = syntaxNode.Children.Where(x => !x.Trivia).ToList();

			Assert.Equal(Children.Count, childrenSelect.Count);
			for (var I = 0; I < childrenSelect.Count; ++I)
				Children[I].Validate(childrenSelect[I], validateTrivia);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Typemaker.Ast.Tests
{
	sealed class SyntaxNodeValidator<TValidateType>: ISyntaxNodeValidator where TValidateType : ISyntaxNode
	{
		public List<ISyntaxNodeValidator> Children { get; set; } = new List<ISyntaxNodeValidator>();


		readonly Type type;
		readonly Func<TValidateType, bool> validator;

		readonly TokenClass expectedClass;

		readonly string expectedComment;
		readonly ulong? expectedLength;

		public SyntaxNodeValidator(Func<TValidateType, bool> validator = null)
		{
			type = typeof(TValidateType);
			this.validator = validator;
		}

		public SyntaxNodeValidator(TokenClass expectedClass, string expectedComment = null)
		{
			if (expectedClass != TokenClass.MultiLineComment && expectedClass != TokenClass.SingleLineComment)
				throw new ArgumentOutOfRangeException(nameof(expectedClass), expectedClass, "Must be a comment class!");
			this.expectedClass = expectedClass;
			this.expectedComment = expectedComment;
		}
		public SyntaxNodeValidator(TokenClass expectedClass, ulong expectedLength)
		{
			this.expectedClass = expectedClass;
			this.expectedLength = expectedLength;
		}

		public void Validate(ISyntaxNode syntaxNode, bool validateTokens)
		{
			if (syntaxNode == null)
				throw new ArgumentNullException(nameof(syntaxNode));

			if (type == null)
				Assert.True(false, "Not a syntax node validator!");

			Assert.True(syntaxNode is TValidateType, $"Expected {type.Name} got {syntaxNode.GetType().Name}!");

			if (validator != null)
				Assert.True(validator((TValidateType)syntaxNode), $"Validation failed for {type.Name}!");
			
			var J = 0;
			foreach (var I in syntaxNode.Trivia)
			{
				Assert.False(J > Children.Count, "More children than child validators!");
				Children[J].Validate(I, validateTokens);
				++J;
			}
			Assert.True(J == Children.Count, "More child validators than children!");
		}

		public void Validate(ITrivia trivia, bool validateTokens)
		{
			if (trivia.Node != null)
			{
				Validate(trivia.Node, validateTokens);
				return;
			}
			else if (!validateTokens)
				return;

			Assert.True(type == null, "Expected SyntaxNode but got Token!");
			Assert.True(Children.Count == 0, "A token validator must not have child validators!");

			var token = trivia.Token;

			Assert.Equal(expectedClass, token.Class);
			if (expectedComment != null)
				Assert.Equal(expectedComment, token.Text);
			else if (expectedLength.HasValue)
				Assert.Equal(expectedLength.Value, (ulong)token.Text.Length);
		}
	}
}

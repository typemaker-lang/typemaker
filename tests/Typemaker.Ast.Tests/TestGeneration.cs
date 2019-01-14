using System;
using System.Collections.Generic;
using System.IO;
using Typemaker.Ast.Statements;
using Typemaker.Ast.Statements.Expressions;
using Xunit;

namespace Typemaker.Ast.Tests
{
	public class TestGeneration
	{
		ISyntaxTree GenerateSyntaxTreeFromSourceFile(string path, bool allowErrors)
		{
			path = "../../../SourceFiles/" + path;
			ISyntaxTree result;
			IReadOnlyList<ParseError> errors;
			using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
				result = SyntaxTreeFactory.Default.CreateSyntaxTree(fs, path, out errors);
			Assert.False(errors.Count > 0 && !allowErrors, $"{errors.Count} parse errors! First: ({errors[0].Location.Line}, {errors[0].Location.Column}): {errors[0].Description}");
			return result;
		}

		[Fact]
		public void CommentMapEnumSimpleGlobalProc() => new SyntaxNodeValidator<ISyntaxTree>
		{
			Children = {
				new SyntaxNodeValidator<IMapDeclaration>(x => x.MapPath == "include/me/daddy.dmm"),
				new SyntaxNodeValidator<IEnumDefinition>(x => x.Name == "testenum")
				{
					Children =
					{
						new SyntaxNodeValidator<IEnumItem>(x => x.Name == "item1" && x.Expression == null),
						new SyntaxNodeValidator<IEnumItem>(x => x.Name == "item2" && x.Expression == null),
					}
				},
				new SyntaxNodeValidator<IProcDefinition>(x => x.Name == "main" && x.ReturnType == null && x.ObjectPath == null && !x.IsVerb && !x.IsConstructor)
				{
					Children =
					{
						new SyntaxNodeValidator<IBlock>(x => !x.Unsafe)
						{
							Children =
							{
								new SyntaxNodeValidator<IVarDefinition>(x => x.Name == "test" && !x.IsConst)
								{
									Children =
									{
										new SyntaxNodeValidator<INullableType>(x => !x.IsNullable && x.RootType == RootType.String),
										new SyntaxNodeValidator<IStringExpression>(x => x.HasFormatters && !x.HasSideEffects && x.IsCompileTime && x.IsConstant && x.Formatter == "I {0} string")
										{
											Children =
											{
												new SyntaxNodeValidator<IStringExpression>(x => x.HasFormatters && !x.HasSideEffects && x.IsCompileTime && x.IsConstant && x.Formatter == "{0} {1} embedded")
												{
													Children =
													{
														new SyntaxNodeValidator<IStringExpression>(x => !x.HasFormatters && !x.HasSideEffects && x.IsConstant && x.IsCompileTime && x.Formatter == "am"),
														new SyntaxNodeValidator<IStringExpression>(x => x.HasFormatters && !x.HasSideEffects && x.IsConstant && x.IsCompileTime && x.Formatter == "\r\n\ta very {0} \\[\"\"]\r\n\t@{\" good \\\"}\r\n\t")
														{
															Children = {
																new SyntaxNodeValidator<IStringExpression>(x => !x.HasFormatters && !x.HasSideEffects && x.IsConstant && x.IsCompileTime && x.Formatter == "very")
																}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}.Validate(GenerateSyntaxTreeFromSourceFile("CommentMapEnumSimpleGlobalProc.tm", false), false);
	}
}

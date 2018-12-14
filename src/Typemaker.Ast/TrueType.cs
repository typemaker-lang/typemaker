using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Typemaker.Parser;

namespace Typemaker.Ast
{
	class TrueType : SyntaxNode, ITrueType
	{
		public RootType RootType { get; }

		public IObjectPath ObjectPath { get; }

		public INullableType IndexType => RootType == RootType.List || RootType == RootType.Dict ? ChildAs<INullableType>() : null;

		public INullableType MapType => RootType == RootType.Dict ? SelectChildren<INullableType>().Last() : null;
		
		public TrueType(TypemakerParser.True_typeContext context, IEnumerable<SyntaxNode> children) : base(context, children)
		{
			var rootTypeContext = context.root_type();

			//use an if chain here cause it's really complicated
			if (rootTypeContext == null)
			{
				RootType = RootType.Object;
				ParseTreeFormatters.ExtractObjectPath(context.extended_identifier(), true, out var path);
				ObjectPath = path;
				return;
			}

			var enumType = rootTypeContext.enum_type();
			if (enumType != null)
			{
				ObjectPath = new ObjectPath(new List<string> { ParseTreeFormatters.ExtractIdentifier(enumType.IDENTIFIER()) });
				RootType = RootType.Enum;
				return;
			}

			var interfaceType = rootTypeContext.interface_type();
			if(interfaceType != null)
			{
				ObjectPath = new ObjectPath(new List<string> { ParseTreeFormatters.ExtractIdentifier(interfaceType.IDENTIFIER()) });
				RootType = RootType.Interface;
				return;
			}

			var pathType = rootTypeContext.path_type();
			if (pathType != null)
			{
				RootType = pathType.concrete_path() != null ? RootType.ConcretePath : RootType.Path;
				return;
			}
			
			if(rootTypeContext.list_type() != null)
			{
				RootType = RootType.List;
				return;
			}

			if(rootTypeContext.dict_type() != null)
			{
				RootType = RootType.Dict;
				return;
			}

			//switch the token for the remaining ones

			//rest are 1 token, so switch it
			var parseTreeChild = rootTypeContext.children.First();
			var tokenType = (parseTreeChild as ITerminalNode)?.Symbol.Type;
			switch (tokenType)
			{
				case TypemakerLexer.BOOL:
					RootType = RootType.Bool;
					break;
				case TypemakerLexer.RESOURCE:
					RootType = RootType.Resource;
					break;
				case TypemakerLexer.INT:
					RootType = RootType.Int;
					break;
				case TypemakerLexer.FLOAT:
					RootType = RootType.Float;
					break;
				case TypemakerLexer.EXCEPTION:
					RootType = RootType.Exception;
					break;
				case TypemakerLexer.STRING:
					RootType = RootType.String;
					break;
				case null:
					throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "First parse tree child is of type {0}!", parseTreeChild.GetType()));
				default:
					throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Decorator context child is of type {0}!", tokenType));
			}
		}
	}
}


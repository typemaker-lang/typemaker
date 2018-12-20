using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast
{
	public static class DeclarationSetTargets
	{
		public static readonly IReadOnlyDictionary<string, RootType> Map = new Dictionary<string, RootType>
		{
			{ "parent_type", RootType.Path },
			{ "autoconvert_resource", RootType.Bool },
			{ "self_math", RootType.Bool }
		};
	}
}

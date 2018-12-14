using System.Collections.Generic;

namespace Typemaker.Ast
{
	public interface IObjectPath
	{
		IReadOnlyList<string> Parts { get; }
	}
}

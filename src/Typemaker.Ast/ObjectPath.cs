using System;
using System.Collections.Generic;
using System.Linq;

namespace Typemaker.Ast
{
	public sealed class ObjectPath : IObjectPath
	{
		public IReadOnlyList<string> Parts { get; }

		public ObjectPath(IEnumerable<string> parts)
		{
			Parts = parts?.ToList() ?? throw new ArgumentNullException(nameof(parts));
		}
	}
}
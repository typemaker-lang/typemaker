using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.ObjectTree
{
	sealed class EnumItem : IEnumItem
	{
		public string StringValue { get; }

		public int? IntValue { get; }

		public string Name { get; }

		public Highlight Location { get; }

		EnumItem(string name, Highlight location)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Location = location ?? throw new ArgumentNullException(nameof(location));
		}

		public EnumItem(string stringValue, string name, Highlight location) : this(name, location)
		{
			StringValue = stringValue;
		}

		public EnumItem(int intValue, string name, Highlight location) : this(name, location)
		{
			IntValue = intValue;
		}
	}
}

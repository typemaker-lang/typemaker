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

		public Location Location { get; }

		EnumItem(string name, Location location)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Location = location ?? throw new ArgumentNullException(nameof(location));
		}

		public EnumItem(string stringValue, string name, Location location) : this(name, location)
		{
			StringValue = stringValue;
		}

		public EnumItem(int intValue, string name, Location location) : this(name, location)
		{
			IntValue = intValue;
		}
	}
}

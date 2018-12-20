using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typemaker.ObjectTree
{
	sealed class EnumDeclaration : IEnumDeclaration
	{
		public string Name { get; }
		public IReadOnlyList<IEnumItem> Values { get; }

		public Highlight Location { get; }

		public EnumDeclaration(string name, Highlight location, IEnumerable<IEnumItem> values)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Location = location ?? throw new ArgumentNullException(nameof(location));
			Values = values?.ToList() ?? throw new ArgumentNullException(nameof(values));
		}

		public IEnumerable<ObjectTreeError> Validate()
		{
			bool? intMode = null;
			var seenNames = new List<string>();
			foreach (var I in Values)
			{
				if (I.StringValue != null)
				{
					if (intMode == true)
						yield return new ObjectTreeError
						{
							Description = "string value in an integer enum",
							Code = ObjectTreeErrorCode.StringInIntEnum,
							Location = I.Location
						};
					else if (intMode == null)
						intMode = false;
				}
				else if (intMode == false)
					yield return new ObjectTreeError
					{
						Description = "integer value in a string enum",
						Code = ObjectTreeErrorCode.IntInStringEnum,
						Location = I.Location
					};
				else
					intMode = true;

				if (seenNames.Any(x => I.Name == x))
					yield return new ObjectTreeError
					{
						Description = "duplicate enum item definition",
						Code = ObjectTreeErrorCode.EnumItemNameCollision,
						Location = I.Location
					};
				else
					seenNames.Add(I.Name);
			}
		}
	}
}

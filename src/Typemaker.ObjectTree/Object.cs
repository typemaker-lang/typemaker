using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Typemaker.ObjectTree
{
	class Object : ObjectDeclarationHolder, IObject
	{
		public bool IsRooted { get; }

		public bool IsPartial { get; }

		public bool SelfMath { get; }

		public bool AutoconvertResource { get; }

		public ObjectVirtuality Virtuality { get; }

		public IObject ParentType { get; }

		public IReadOnlyList<Highlight> Locations => locations;

		public IReadOnlyList<IObject> Subtypes => subtypes;

		public string FullyExtendedName => GetLongName(false);

		public string FullyQualifiedName => GetLongName(true);

		public IReadOnlyList<IObjectProcDefinition> Procs => procs;

		readonly List<IObjectProcDefinition> procs;
		readonly List<Highlight> locations;
		readonly List<IObject> subtypes;

		protected Object(string name, IObject parent, ObjectVirtuality objectVirtuality, bool rooted, bool partial) : base(name)
		{
			ParentType = parent;
			Virtuality = objectVirtuality;
			IsRooted = rooted;
			IsPartial = partial;

			procs = new List<IObjectProcDefinition>();
			locations = new List<Highlight>();
			subtypes = new List<IObject>();
		}

		public Object(string name, IObject parent, Highlight initialLocation, ObjectVirtuality objectVirtuality, bool rooted, bool partial, bool selfMath, bool autoconvertResource) : this(name, parent, objectVirtuality, rooted, partial)
		{
			if (parent == null)
				throw new ArgumentNullException(nameof(parent));
			if (initialLocation == null)
				throw new ArgumentNullException(nameof(initialLocation));

			SelfMath = selfMath;
			AutoconvertResource = autoconvertResource;

			parent.AddSubtype(this);

			locations.Add(initialLocation);
		}

		string GetLongName(bool ignoreRoot)
		{
			var builder = new StringBuilder();
			for (IObject current = this; current != null && (!ignoreRoot || !current.IsRooted); current = current.ParentType)
			{
				builder.Insert(0, current.Name);
				builder.Insert(0, '/');
			}
			return builder.ToString();
		}

		public override IEnumerable<ObjectTreeError> Validate()
		{
			yield return ValidateImplements(null);
			if (!IsPartial && Locations.Count > 1)
				yield return new ObjectTreeError
				{
					Code = ObjectTreeErrorCode.PartialNonPartial,
					Description = String.Format(CultureInfo.InvariantCulture, "{0} is declared in serveral locations but is not declared as partial", FullyExtendedName),
					Location = Locations.First()
				};
		}

		public void AddLocation(Highlight location) => locations.Add(location ?? throw new ArgumentNullException(nameof(location)));

		public void AddSubtype(IObject subtype) => subtypes.Add(subtype ?? throw new ArgumentNullException(nameof(subtype)));

		void IRemovableChildren.RemoveFileItems(string filePath)
		{
			RemoveFileItems(filePath);
			
			foreach (var I in Subtypes)
				I.RemoveFileItems(filePath);

			subtypes.RemoveAll(x => x.Locations.Count == 0);
		}
	}
}

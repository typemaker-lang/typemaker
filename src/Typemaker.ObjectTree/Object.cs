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

		public ObjectVirtuality Virtuality { get; }

		public IObject ParentType { get; }

		public IConstructor Constructor { get; private set; }

		public IReadOnlyList<ILocatable> Locations => locations;

		public IReadOnlyList<IObject> Subtypes => subtypes;

		public string FullyExtendedName => GetLongName(false);

		public string FullyQualifiedName => GetLongName(true);

		readonly List<ILocatable> locations;
		readonly List<IObject> subtypes;

		protected Object(string name, IObject parent, ObjectVirtuality objectVirtuality, bool rooted, bool partial) : base(name)
		{
			ParentType = parent;
			Virtuality = objectVirtuality;
			IsRooted = rooted;
			IsPartial = partial;

			locations = new List<ILocatable>();
			subtypes = new List<IObject>();
		}

		public Object(string name, IObject parent, ILocatable initialLocation, ObjectVirtuality objectVirtuality, bool rooted, bool partial) : this(name, parent, objectVirtuality, rooted, partial)
		{
			if (parent == null)
				throw new ArgumentNullException(nameof(parent));
			if (initialLocation == null)
				throw new ArgumentNullException(nameof(initialLocation));

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

		public void AddLocation(ILocatable location) => locations.Add(location ?? throw new ArgumentNullException(nameof(location)));

		public void AddSubtype(IObject subtype) => subtypes.Add(subtype ?? throw new ArgumentNullException(nameof(subtype)));

		public void AddConstructor(IConstructor constructor)
		{
			if (constructor == null)
				throw new ArgumentNullException(nameof(constructor));
			if (Constructor != null)
				throw new InvalidOperationException("Constructor has already been set!");
			Constructor = constructor;
		}

		void IRemovableChildren.RemoveFileItems(string filePath)
		{
			RemoveFileItems(filePath);

			Constructor = Constructor?.FixParentChainAfterFileRemoval(filePath);

			foreach (var I in Subtypes)
				I.RemoveFileItems(filePath);

			subtypes.RemoveAll(x => x.Locations.Count == 0);
		}
	}
}

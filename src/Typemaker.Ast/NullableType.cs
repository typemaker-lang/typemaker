using System;
using System.Collections.Generic;
using System.Linq;

namespace Typemaker.Ast
{
	sealed class NullableType : SyntaxNode, INullableType
	{
		public bool IsNullable { get; }

		public RootType RootType { get; }

		public IObjectPath ObjectPath { get; }

		public INullableType IndexType => mapDefinitionType == MapDefinitionType.IndexOnly || mapDefinitionType == MapDefinitionType.FullyDefined ? ChildAs<INullableType>() : null;

		public INullableType MapType => mapDefinitionType == MapDefinitionType.ValueOnly || mapDefinitionType == MapDefinitionType.FullyDefined ? ChildrenAs<INullableType>().Last() : null;

		readonly MapDefinitionType? mapDefinitionType;
		
		NullableType(bool isNullable, IEnumerable<ITrivia> children) : base(children)
		{
			IsNullable = isNullable;
		}

		public NullableType(RootType rootType, MapDefinitionType? mapDefinitionType, bool isNullable, IEnumerable<ITrivia> children) : this(isNullable, children)
		{
			if (rootType == RootType.Object || rootType == RootType.Enum || rootType == RootType.Interface)
				throw new ArgumentOutOfRangeException(nameof(rootType), rootType, "Use other constructor for objects!");

			if (mapDefinitionType.HasValue && rootType != RootType.List && rootType != RootType.Dict)
				throw new ArgumentOutOfRangeException(nameof(rootType), rootType, "Invalid RootType for MapDefinitionType!");
			if (!mapDefinitionType.HasValue && (rootType == RootType.List || rootType == RootType.Dict))
				throw new ArgumentOutOfRangeException(nameof(rootType), rootType, "Invalid RootType for non-MapDefinitionType!");

			RootType = rootType;
			this.mapDefinitionType = mapDefinitionType;
		}

		public NullableType(IObjectPath objectPath, RootType rootType, bool isNullable, IEnumerable<ITrivia> children) : this (isNullable, children)
		{
			if (!(rootType == RootType.Object || rootType == RootType.Enum || rootType == RootType.Interface))
				throw new ArgumentOutOfRangeException(nameof(rootType), rootType, "Use other constructor for non-objects!");
			ObjectPath = objectPath ?? throw new ArgumentNullException(nameof(objectPath));
			RootType = rootType;
		}
	}
}

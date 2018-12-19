using System;
using System.Collections.Generic;
using System.Linq;
using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	sealed class ObjectTree : IObjectTree
	{
		public IObject RootObject { get; }

		public IEnumerable<IObject> RootedObjects => GetRootedObjects();

		public IReadOnlyList<IVariableDeclaration> Variables => variables;

		public IReadOnlyList<IProcDeclaration> Procs => procs;

		public IReadOnlyList<IEnumDeclaration> Enums => enums;

		public IReadOnlyList<IInterface> Interfaces => interfaces;

		readonly List<IVariableDeclaration> variables;
		readonly List<IProcDeclaration> procs;
		readonly List<IEnumDeclaration> enums;
		readonly List<IInterface> interfaces;

		public ObjectTree(IObject rootObject)
		{
			RootObject = rootObject ?? throw new ArgumentNullException(nameof(rootObject));

			variables = new List<IVariableDeclaration>();
			procs = new List<IProcDeclaration>();
			enums = new List<IEnumDeclaration>();
			interfaces = new List<IInterface>();
		}

		IEnumerable<IObject> GetRootedObjects()
		{
			IEnumerable<IObject> Search(IObject current)
			{
				if (current.IsRooted)
					yield return current;
				foreach (var I in current.Subtypes)
					foreach (var J in Search(I))
						yield return J;
			}

			return Search(RootObject);
		}

		public IObject LookupPath(ObjectPath path)
		{
			if (path == null)
				throw new ArgumentNullException(nameof(path));

			if (path.Parts.Count == 0)
				throw new InvalidOperationException("Path is empty!");

			IObject currentObject = null;
			IEnumerable<IObject> search = RootedObjects;
			foreach(var I in path.Parts)
			{
				currentObject = search.Where(x => x.Name == I).FirstOrDefault();
				search = currentObject?.Subtypes;
				if (search == null)
					return currentObject;
			}

			return currentObject;
		}

		public void RemoveFileItems(string filePath)
		{
			if (filePath == null)
				throw new ArgumentNullException(nameof(filePath));

			void RemoveItems<TLocatable>(List<TLocatable> list) where TLocatable : ILocatable => list.RemoveAll(x => x.FilePath == filePath);

			RootObject.RemoveFileItems(filePath);
			RemoveItems(variables);
			RemoveItems(enums);
			RemoveItems(interfaces);
			RemoveItems(procs);
		}
	}
}

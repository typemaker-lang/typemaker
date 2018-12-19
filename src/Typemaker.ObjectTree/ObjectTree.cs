using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Typemaker.Ast;

namespace Typemaker.ObjectTree
{
	sealed class ObjectTree : IObjectTree
	{
		public IObject RootObject { get; }

		public IEnumerable<IObject> RootedObjects => GetRootedObjects();

		public IReadOnlyList<IVariableDeclaration> Variables => variables;

		public IReadOnlyList<IProcDefinition> Procs => procs;

		public IReadOnlyList<IEnumDeclaration> Enums => enums;

		public IReadOnlyList<IInterface> Interfaces => interfaces;

		readonly List<IVariableDeclaration> variables;
		readonly List<IProcDefinition> procs;
		readonly List<IEnumDeclaration> enums;
		readonly List<IInterface> interfaces;

		public ObjectTree(IObject rootObject)
		{
			RootObject = rootObject ?? throw new ArgumentNullException(nameof(rootObject));

			variables = new List<IVariableDeclaration>();
			procs = new List<IProcDefinition>();
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

			RootObject.RemoveFileItems(filePath);

			void RemoveItems<TLocatable>(List<TLocatable> list) where TLocatable : ILocatable => list.RemoveAll(x => x.Location.FilePath == filePath);

			RemoveItems(variables);
			RemoveItems(enums);
			RemoveItems(interfaces);
			RemoveItems(procs);
		}

		public void AddVariable(IVariableDeclaration variable) => variables.Add(variable ?? throw new ArgumentNullException(nameof(variable)));

		public void AddProc(IProcDefinition proc) => procs.Add(proc ?? throw new ArgumentNullException(nameof(proc)));

		public void AddEnum(IEnumDeclaration enumDeclaration) => enums.Add(enumDeclaration ?? throw new ArgumentNullException(nameof(enumDeclaration)));

		public void AddInterface(IInterface interfaceDefinition) => interfaces.Add(interfaceDefinition ?? throw new ArgumentNullException(nameof(interfaceDefinition)));

		public IEnumerable<ObjectTreeError> Validate()
		{
			ObjectTreeError CheckNameCollisions<TLocdentifiable>(string typeName, ObjectTreeErrorCode failCode, IEnumerable<TLocdentifiable> things, Func<TLocdentifiable, string> getThingName) where TLocdentifiable : ILocatable
			{
				var seenNames = new List<string>();
				foreach (var I in things)
				{
					var thingsName = getThingName(I);
					if (seenNames.Any(x => thingsName == x))
						return new ObjectTreeError
						{
							Code = failCode,
							Description = String.Format(CultureInfo.InvariantCulture, "Multiple definitions of {0} {1} exist", typeName, thingsName),
							Location = I.Location
						};
					seenNames.Add(thingsName);
				}
				return null;
			}

			var result = CheckNameCollisions("interface", ObjectTreeErrorCode.InterfaceNameCollision, Interfaces, x => x.Name);
			if (result != null)
				yield return result;
			result = CheckNameCollisions("enum", ObjectTreeErrorCode.EnumNameCollision, Enums, x => x.Name);
			if (result != null)
				yield return result;
			result = CheckNameCollisions("global proc", ObjectTreeErrorCode.GlobalProcNameCollision, Procs, x => x.Declaration.Name);
			if (result != null)
				yield return result;
			result = CheckNameCollisions("global variable", ObjectTreeErrorCode.GlobalVarNameCollision, Variables, x => x.Name);
			if (result != null)
				yield return result;

			foreach (var I in Enums.SelectMany(x => x.Validate()))
				yield return I;

			foreach (var I in Interfaces.SelectMany(x => x.Validate()))
				yield return I;

			foreach (var I in RootObject.Validate())
				yield return I;
		}
	}
}

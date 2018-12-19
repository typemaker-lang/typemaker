using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Typemaker.ObjectTree
{
	abstract class ObjectDeclarationHolder : IObjectDeclarationHolder
	{
		public string Name { get; }

		public IReadOnlyList<IObjectVariableDeclaration> Variables => variables;

		public IReadOnlyList<IObjectProcDeclaration> Procs => procs;

		public IReadOnlyList<IInterface> Implements => implements;

		readonly List<IObjectVariableDeclaration> variables;
		readonly List<IObjectProcDeclaration> procs;
		readonly List<IInterface> implements;
		
		static ObjectTreeError ValidateImplements(IEnumerable<IInterface> interfaces, IInterface duplicate, List<IInterface> chain)
		{
			foreach (var I in interfaces)
			{
				if (I == duplicate)
					return new ObjectTreeError
					{
						Code = ObjectTreeErrorCode.ImplementCycle,
						Description = String.Format(CultureInfo.InvariantCulture, "Implements cycle {0}", String.Join(" -> ", chain.Select(x => x.Name)))
					};
				ValidateImplements(I.Implements, duplicate, new List<IInterface>(chain) { I });
			}
			return null;
		}

		protected ObjectDeclarationHolder(string name)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));

			variables = new List<IObjectVariableDeclaration>();
			procs = new List<IObjectProcDeclaration>();
			implements = new List<IInterface>();
		}

		public void AddVariable(IObjectVariableDeclaration variable) => variables.Add(variable ?? throw new ArgumentNullException(nameof(variable)));
		public void AddProc(IObjectProcDeclaration proc) => procs.Add(proc ?? throw new ArgumentNullException(nameof(proc)));
		public void AddImplement(IInterface implement)
		{
			ValidateImplements(implements, implement ?? throw new ArgumentNullException(nameof(implement)), new List<IInterface>());
			implements.Add(implement);
		}

		protected ObjectTreeError ValidateImplements(IInterface start) => ValidateImplements(implements, start, new List<IInterface>());
		public abstract IEnumerable<ObjectTreeError> Validate();

		protected void RemoveFileItems(string filePath)
		{
			if (filePath == null)
				throw new ArgumentNullException(nameof(filePath));

			variables.RemoveAll(x => x.FilePath == filePath);
			implements.RemoveAll(x => x.FilePath == filePath);

			var newProcs = Procs.Select(x => x.FixParentChainAfterFileRemoval(filePath)).ToList();
			procs.Clear();
			procs.Capacity = newProcs.Count;
			procs.AddRange(newProcs);
		}
	}
}

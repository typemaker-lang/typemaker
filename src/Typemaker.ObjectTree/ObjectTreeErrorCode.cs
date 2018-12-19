using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.ObjectTree
{
	public enum ObjectTreeErrorCode
	{
		ImplementCycle,
		PartialNonPartial,
		InterfaceNameCollision,
		EnumNameCollision,
		GlobalVarNameCollision,
		GlobalProcNameCollision
	}
}

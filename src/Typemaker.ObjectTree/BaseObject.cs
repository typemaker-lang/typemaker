namespace Typemaker.ObjectTree
{
	sealed class BaseObject : Object
	{
		public BaseObject() : base("__typemaker_base_object", null, ObjectVirtuality.Abstract, true, false)
		{

		}
	}
}
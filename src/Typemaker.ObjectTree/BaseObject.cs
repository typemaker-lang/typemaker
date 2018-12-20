namespace Typemaker.ObjectTree
{
	sealed class BaseObject : Object
	{
		public BaseObject() : base("__tm_base_object", null, ObjectVirtuality.Abstract, true, false)
		{

		}
	}
}
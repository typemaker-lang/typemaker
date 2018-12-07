namespace Typemaker.Ast
{
	public interface IFileLocation
	{
		string Path { get; }
		ulong Line { get; }
		ulong Column { get; }
	}
}

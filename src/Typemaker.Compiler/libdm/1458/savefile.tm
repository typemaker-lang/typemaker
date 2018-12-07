declare sealed /savefile {
	set parent_type = /__tm_base_object

	public var/string/cd;
	public var/list/string/dir;
	public var/int/eof;
	public var/string/name;

	public /proc/ExportText(string/path = cd, file/file = null) -> nullable/string;
	public /proc/Flush() -> void;
	public /proc/ImportText(string/path = cd, string/source = null) -> void;
	public /proc/Lock(int/timeout) -> bool;
	public /proc/Unlock() -> void;
}

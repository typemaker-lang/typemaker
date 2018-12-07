declare partial /regex {
	set parent_type = /datum;
	public var/nullable/string/flags;
	public var/nullable/list/string/group;
	public var/nullable/int/index;
	public var/nullable/string/match;
	public var/string/name;
	public var/nullable/int/next;
	public var/nullable/string/text;

	public /proc/Find(string/haystack, int/Start = 1, int/End = 0) -> int;
	public /proc/Replace(string/haystack, replacement, int/Start = 1, int/End = 0) -> void;
}

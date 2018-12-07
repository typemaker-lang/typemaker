declare partial /datum {
	set parent_type = /__typemaker_base_object;
	
	// New() is handled by the compiler in /__typemaker_base_object
	protected virtual /proc/Del() -> void;
	protected virtual /proc/Read(savefile/F) -> void;
	protected virtual /proc/Write(savefile/F) -> void;
	protected virtual /proc/Topic(string/href, list/href_list) -> void;
}

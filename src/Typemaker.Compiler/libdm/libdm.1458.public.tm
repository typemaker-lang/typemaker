

declare /datum {
	set parent_type = /__typemaker_base_object;
	public var/list/vars;

	public var/nullable/string/tag;
	
	// New() is handled by the compiler in /__typemaker_base_object
	protected virtual /proc/Del() -> void;
	protected virtual /proc/Read(savefile/F) -> void;
	protected virtual /proc/Write(savefile/F) -> void;
	protected virtual /proc/Topic(string/href, list/href_list) -> void;
}

/enum/client_connection {
	dream_seeker = "seeker",
	telnet = "telnet",
	world = "world",
	cgi = "cgi",
	web = "web",
	http = "http",
	unknown = ""	// "An empty value means the connection type is unknown because a full handshake hasn't been completed yet." < Never seen this in practice, hopefully he meant empty string
}

/enum/client_control_freak {
	none = 0,
	all = 1,
	skin = 2,
	macros = 4
}

declare var/const/int/NORTH;
declare var/const/int/SOUTH;
declare var/const/int/EAST;
declare var/const/int/WEST;

declare /client {
	set parent_type = /__typemaker_base_object;

	public readonly var/string/address;
	public readonly var/bool/authenticate;
	//TODO: bounds
	public readonly var/int/byond_build
	public readonly var/int/byond_version;

	public var/nullable/string/command_text;
	public readonly var/enum/client_connection/connection;
	public readonly var/enum/client_control_freak/control_freak;

	public readonly var/string/computer_id;	//check if nullable with web client

	public var/nullable/string/default_verb_category;

	public var/int/dir;
	public var/nullable/string/edge_limit;
	public var/nullable/atom/eye;	//check if nullable
	public var/float/fps;

	public var/string/gender;
	public var/float/glide_size; //check if float
	public readonly var/list/image/images;

	public var/float/tick_lag;
	public var/string/key;

	public var/lazy_eye;

	public var/nullable/mob/mob;

	//WIP NOT DONE
}

declare /atom {
	parent_type = /datum
}
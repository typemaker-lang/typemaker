declare var/const/number/MOB_PERSPECTIVE;
declare var/const/number/EYE_PERSPECTIVE;
declare var/const/number/EDGE_PERSPECTIVE;

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

declare partial /client {
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

	public var/icon/mouse_pointer_icon;

	public var/number/perspective;
	
	public var/number/pixel_x; //check if floats
	public var/number/pixel_y; //check if floats
	public var/number/pixel_z; //check if floats
	public var/number/pixel_w; //check if floats

	public var/preload_rsc;

	public readonly var/list/obj/screen;

	public readonly var/script;	

	public var/bool/show_map;
	public var/bool/show_popup_menus;
	public var/bool/show_verb_panel;

	public var/statobj;

	public var/nullable/string/statpanel;

	public var/float/tick_lag;

	public var/list/path/verbs;

	public var/view;
	
	public readonly var/nullable/atom/virtual_eye;

	public /proc/AllowUpload(string/filename, int/filelength) -> bool;
	public /proc/Center() -> void;
	public /proc/CheckPassport(string/passport_identifier) -> int;
	public /proc/Click(atom/object, atom/location, string/control, string/params) -> void;
	public /proc/Command(string/command) -> void;
	public /proc/DblClick(atom/object, atom/location, string/control, string/params) -> void;
	public /proc/Del() -> void;
	public /proc/East() -> bool;
	public /proc/Export(nullable/file/file = null) -> void;
	public /proc/Import() -> nullable/file;
	public /proc/IsByondMember() -> bool;

	//WIP NOT DONE
}

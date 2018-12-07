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
	set parent_type = /__typemaker_base_object
}

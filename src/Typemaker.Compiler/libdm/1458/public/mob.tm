declare var/const/int/SEE_INFRA;
declare var/const/int/SEE_SELF;
declare var/const/int/SEE_MOBS;
declare var/const/int/SEE_OBJS;
declare var/const/int/SEE_TURFS;
declare var/const/int/SEE_PIXELS;
declare var/const/int/SEE_THRU;
declare var/const/int/SEE_BLACKNESS;
declare var/const/int/BLIND;

declare partial /mob {
	public var/nullable/string/ckey;
	public var/nullable/client/client;
	public var/readonly/list/mob/group;
	public var/nullable/string/key;
	public var/int/see_in_dark;
	public var/bool/see_infrared;
	public var/int/see_invisible;
	public var/int/sight;
	
	public /proc/Login() -> void;
	public /proc/Logout() -> void;
}

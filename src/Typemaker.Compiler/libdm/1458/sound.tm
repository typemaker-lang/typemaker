declare var/const/int/SOUND_MUTE;
declare var/const/int/SOUND_PAUSED;
declare var/const/int/SOUND_STREAM;
declare var/const/int/SOUND_UPDATE;

declare partial /sound {
	set parent_type = /datum;
	set autoconvert_resource = TRUE;

	public var/file;
	public var/int/repeat;
	public var/bool/wait;
	public var/int/channel;
	public var/int/volume;
	public var/int/frequency;
	public var/int/pan;
	public var/int/priority;
	
	public var/int/x;
	public var/int/y;
	public var/int/z;

	public var/float/falloff;

	public var/environment;

	public var/nullable/list/float/echo;
}

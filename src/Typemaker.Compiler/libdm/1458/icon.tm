declare var/const/int/ICON_ADD;
declare var/const/int/ICON_SUBTRACT;
declare var/const/int/ICON_MULTIPLY;
declare var/const/int/ICON_OVERLAY;
declare var/const/int/ICON_AND;
declare var/const/int/ICON_OR;
declare var/const/int/ICON_UNDERLAY;

declare partial /icon {
	set parent_type = /datum;
	set autoconvert_resource = true;
	set self_math = true;

	public /proc/Blend(icon/icon, number/function = ICON_ADD, int/x = 1, int/y = 1) -> void;
	public /proc/Crop(int/x1, int/y1, int/x2, int/y2) -> void;
	public /proc/DrawBox(string/rgb, int/x1, int/y1, int/x2 = x1, int/y2 = y1) -> void;
	public /proc/Flip(int/dir) -> void;
	public /proc/GetPixel(int/x, int/y, nullable/string/icon_state, int/dir = 0, int/frame = 0, nullable/int/moving = -1) -> nullable/string;
	public /proc/Height() -> int;
	public /proc/IconStates(number/mode = 0) -> list/string;

	//WIP
}

declare partial /movable {
	public var/int/animate_movement;
	public var/int/bound_x;
	public var/int/bound_y;
	public var/int/bound_width;
	public var/int/bound_height;
	public var/list/atom/locs;
	public var/float/glide_size;
	public var/nullable/string/screen_loc;
	public var/int/step_size;
	public var/int/step_x;
	public var/int/step_y;
	
	public /proc/Bump(atom/Obstacle) -> void;
	public /proc/Cross(atom/movable/O) -> bool;
	public /proc/Crossed(atom/movable/O) -> void;
	public /proc/Move(atom/NewLoc, int/Dir = 0, int/step_x = 0, int/step_y = 0) -> int;
	public /proc/Uncross(atom/movable/O) -> bool;
	public /proc/Uncrossed(atom/movable/O) -> void;
}

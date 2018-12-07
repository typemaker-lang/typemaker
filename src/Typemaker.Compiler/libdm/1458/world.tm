declare var/const/world/world;

declare partial abstract sealed /world {
	set parent_type = /__tm_base_object;

	protected /New() -> void;
}

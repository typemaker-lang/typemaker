declare partial /world {
	public readonly var/nullable/string/address;
	public readonly var/path/concrete/area;
	public readonly var/int/byond_build;
	public readonly var/int/byond_version;
	public readonly var/int/cache_lifespan;

	public readonly var/list/atom/contents;

	public readonly var/float/cpu;
	public readonly var/nullable/string/executor;

	public var/int/fps;

	public var/bool/game_state;
	
	public readonly var/nullable/string/host;
	
	public readonly var/nullable/string/hub;
	public readonly var/nullable/string/hub_password;

	//WIP
}

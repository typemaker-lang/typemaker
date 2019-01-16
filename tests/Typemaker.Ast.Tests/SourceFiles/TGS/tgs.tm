//tgstation-server DMAPI

//All functions and datums outside this document are subject to change with any version and should not be relied on

//EVENT CODES

/enum/tgs_event {
	PortSwap = -2,
	RebootModeChange = -1,
	/// <summary>
	/// Parameters: Reference name, commit sha
	/// </summary>
	RepoResetOrigin = 0,

	/// <summary>
	/// Parameters: Checkout target
	/// </summary>
	RepoCheckout = 1,

	/// <summary>
	/// No parameters
	/// </summary>
	RepoFetch = 2,

	/// <summary>
	/// Parameters: Pull request number, pull request sha, merger message
	/// </summary>
	RepoMergePullRequest = 3,

	/// <summary>
	/// Parameters: Absolute path to repository root
	/// </summary>
	RepoPreSynchronize = 4,

	/// <summary>
	/// Parameters: Version being installed
	/// </summary>
	ByondInstallStart = 5,

	/// <summary>
	/// Parameters: Error string
	/// </summary>
	ByondInstallFail = 6,

	/// <summary>
	/// Parameters: Old active version, new active version
	/// </summary>
	ByondActiveVersionChange = 7,

	/// <summary>
	/// Parameters: Game directory path, origin commit sha
	/// </summary>
	CompileStart = 8,

	/// <summary>
	/// No parameters
	/// </summary>
	CompileCancelled = 9,

	/// <summary>
	/// Parameters: Game directory path, "1" if compile succeeded and api validation failed, "0" otherwise
	/// </summary>
	CompileFailure = 10,

	/// <summary>
	/// Parameters: Game directory path
	/// </summary>
	CompileComplete = 11,

	/// <summary>
	/// No parameters
	/// </summary>
	InstanceAutoUpdateStart = 12,

	/// <summary>
	/// Parameters: Base sha, target sha, base reference, target reference
	/// </summary>
	RepoMergeConflict = 13
}

//OTHER ENUMS

/enum/tgs_reboot_mode {
	Normal = 0,
	Shutdown = 1,
	Restart = 2
}

/enum/tgs_security {
	Trusted = 0,
	Safe = 1,
	Ultrasafe = 2
}

//REQUIRED HOOKS

//Call this somewhere in /world/New() that is always run
//event_handler: optional user defined event handler. The default behaviour is to broadcast the event in english to all connected admin channels
//minimum_required_security_level: The minimum required security level to run the game in which the DMAPI is integrated
precedence(-1) /world/proc/TgsNew(datum/tgs_event_handler/event_handler, minimum_required_security_level = TGS_SECURITY_ULTRASAFE) -> void
	return;

//Call this when your initializations are complete and your game is ready to play before any player interactions happen
//This may use world.sleep_offline to make this happen so ensure no changes are made to it while this call is running
//Most importantly, before this point, note that any static files or directories may be in use by another server. Your code should account for this
//This function should not be called before ..() in /world/New()
precedence(-1) /world/proc/TgsInitializationComplete() -> void
	return;

//DATUM DEFINITIONS

//represents git revision information about the current world build
/datum/tgs_revision_information {
	readonly var/nullable/string/commit;			//full sha of compiled commit
	readonly var/nullable/string/origin_commit;	//full sha of last known remote commit. This may be null if the TGS repository is not currently tracking a remote branch
}

//represents a version of tgstation-server
/datum/tgs_version {
	readonly var/int/suite;			//The suite version, can be >=3

	//this group of variables can be null to represent a wild card
	readonly var/nullable/int/major;					//The major version
	readonly var/nullable/int/minor;					//The minor version
	readonly var/nullable/int/patch;					//The patch version
	
	readonly var/string/raw_parameter;			//The unparsed parameter
	readonly var/string/deprefixed_parameter;	//The version only bit of raw_parameter
}

//if the tgs_version is a wildcard version
/datum/tgs_version/proc/Wildcard() -> bool
	return;

//represents a merge of a GitHub pull request
/datum/tgs_revision_information/test_merge {
	readonly var/int/number;				//pull request number
	readonly var/string/title;				//pull request title
	readonly var/string/body;				//pull request body
	readonly var/string/author;				//pull request github author
	readonly var/string/url;					//link to pull request html
	readonly var/string/pull_request_commit;	//commit of the pull request when it was merged
	readonly var/nullable/string/time_merged;			//timestamp of when the merge commit for the pull request was created
	readonly var/nullable/string/comment;				//optional comment left by the one who initiated the test merge
}

//represents a connected chat channel
/datum/tgs_chat_channel {
	readonly var/string/id;					//internal channel representation
	readonly var/string/friendly_name;		//user friendly channel name
	readonly var/string/connection_name;		//the name of the configured chat connection
	readonly var/bool/is_admin_channel;	//if the server operator has marked this channel for game admins only
	readonly var/bool/is_private_channel;	//if this is a private chat channel
	readonly var/nullable/string/custom_tag;					//user defined string associated with channel
}	

//represents a chat user
/datum/tgs_chat_user {
	readonly var/string/id;							//Internal user representation, requires channel to be unique
	readonly var/string/friendly_name;				//The user's public name
	readonly var/string/mention;						//The text to use to ping this user in a message
	readonly var/datum/tgs_chat_channel/channel;	//The /datum/tgs_chat_channel this user was from
}

//user definable callback for handling events
//extra parameters may be specified depending on the event
precedence(-1) /datum/tgs_event_handler/proc/HandleEvent(int/event_code, ...) -> void {
	set waitfor = FALSE;
	return;
}

//user definable chat command
/datum/tgs_chat_command {
	readonly var/string/name;			//the string to trigger this command on a chat bot. e.g. TGS3_BOT: do_this_command
	readonly var/string/help_text;		//help text for this command
	readonly var/bool/admin_only;	//set to TRUE if this command should only be usable by registered chat admins
}

//override to implement command
//sender: The tgs_chat_user who send to command
//params: The trimmed string following the command name
//The return value will be stringified and sent to the appropriate chat
precedence(-1) /datum/tgs_chat_command/proc/Run(datum/tgs_chat_user/sender, params) -> nullable/string {
	CRASH("[type] has no implementation for Run()");
}

/*
The MIT License

Copyright (c) 2017 Jordan Brown

Permission is hereby granted, free of charge, 
to any person obtaining a copy of this software and 
associated documentation files (the "Software"), to 
deal in the Software without restriction, including 
without limitation the rights to use, copy, modify, 
merge, publish, distribute, sublicense, and/or sell 
copies of the Software, and to permit persons to whom 
the Software is furnished to do so, 
subject to the following conditions:

The above copyright notice and this permission notice 
shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR 
ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

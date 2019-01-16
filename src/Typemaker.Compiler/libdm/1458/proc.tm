declare /proc/abs(float/A) -> float;

declare /proc/addtext(string/Arg1, string/Arg2, string/...) -> string;

declare yield /proc/alert(mob/Usr = usr, nullable/string/Message, nullable/string/Title = null, string/Button1 = "Ok", nullble/string/Button2 = null, nullable/string/Button3 = null);

declare /proc/animate(...) -> void;

declare /proc/arccos(float/X) -> float;

declare /proc/arcsin(float/X) -> float;

declare /proc/ascii2text(int/N) -> string;

declare /proc/block(turf/Start, turf/End) -> list/turf;

declare /proc/bounds_dist(atom/Ref, atom/Target) -> int;

declare /proc/browse(Body, nullable/string/Options = null);

declare /proc/browse_rsc(resource/File, string/FileName);

declare /proc/ckey(string/Key) -> nullable/string;

declare /proc/ckeyEx(string/Text) -> nullable/string;

declare /proc/cmptext(string/T1, string/T2, string/...) -> bool;

declare /proc/cmptextEx(string/T1, string/T2, string/...) -> bool;

declare /proc/copytext(string/T, int/Start = 1, int/End = 0) -> string;

declare /proc/cos(float/X) -> float;

declare /proc/del(Object) -> void;

declare /proc/fcopy(Src, string/Dest) -> bool;

declare /proc/fcopy_rsc(File) -> nullable/resource;

declare /proc/fdel(string/File) -> bool;

declare /proc/fexists(string/File) -> bool;

declare /proc/initial(prototype);

declare /proc/prob(float/P) -> bool;

declare yield /proc/sleep(float/) -> void;

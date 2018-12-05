
explicit /datum/example {
    //none of these vars, procs, or the datum will be optimized out
    var/int/x = 4;
}

/datum/some_things_eliminated {
    var/int/wont_be_elimiated_because_of_proc_foo = 4;
    var/int/wont_be_elimiated_because_of_proc_WontBeEliminated = 4;

    var/nullable/string/this_will_be_eliminated;
    explicit var/nullable/string/this_wont_be_elimiated;
}

/datum/some_things_eliminated/proc/WillBeElminated() -> void {}

explicit /datum/some_things_eliminated/proc/WontBeElminated() -> void {
    wont_be_elimiated_because_of_proc_WontBeEliminated = 5;
}

/datum/some_things_eliminated/proc/WontBeElminatedBecauseOfProcFoo() -> void {}

//won't be eliminated
explicit /proc/foo() -> void {
    var/datum/some_things_elimiated/D = ;
    D.wont_be_elimiated_because_of_proc_foo = 5;
    D.WontBeElminatedBecauseOfProcFoo();
}

# Typemaker

Typemaker is a language designed to improve scale and maintainibility of BYOND's DreamMaker programming language

# Goals

Insert deep philosophy about how /tg/ coders hurt Cyberboss' feelings

No but seriously, I just wanna make the project more maintainable

# Features

## C-Style Blocks

Statements must end with `;`s. Blocks must either use `{}`s or be a single statement. Entire program may be written on one line

```dm
/proc/ThisIsAValidProcDefinition() -> void world << "Hello";

/proc/SoIsThis() -> string {
    return "asdf"
}
```

## Strong Typing

All vars are implicitly prefixed. Manual prefixing is required if type deduction isn't obvious. Casting is only implcit for int -> float. All other usages must match.

Proc parameters forced to follow the `type/name` syntax without leading slash and are validated at the call site if no default values exist

New prefixes: `/file`, `/resource`, `/bool`, `/string`, `/path`, `/int`, `/float`, `/interface`. `/nullable` comes before any prefix if the variable may be `null`. `/path/concrete` limits to non-abstract paths and is the only `/path` type usable in `new` statements, `/list/<another path>` now allows accessing the list strongly

No default initialization for `/string` and `/path`

Intermixing `int`s and `float`s converts the result type to a float.

All paths must be absolute

Non-nullable var types in datum definitions must be initialized in `/New()`

### Declarative Return Types

```dm
/datum/example/proc/Foo() -> /datum/bar {
    //errors if missing or incorrect return statement
}

/atom/proc/MoveLeft() -> /nullable/int {
    if(prob(50))
        return null;
    return 4;
}

/proc/lemon() -> void {

}
```

### Enums

The `/enum` type cannot be used on it's own and represents a strongly typed set of values. May be freely converted to and from their backing type (int or string). Automatic incrementing int's by default. If strings, value must be declared

```dm
/enum/Thing {
	A,	//default 0
	B,	//default 1
	C = 17,
	D	//default 18
}

/enum/StringEnum {
	A = "asdf",
	B = "fdsa"
}

/proc/Example() -> void {
	var/enum/Thing/X = /enum/Thing/C;
	var/int/C = X;
	C += 20;
	//no backcast validation
	X = C;
}
```

### Nameof

`nameof()` simply takes any identifier and stringifies the most significant portion of it

```dm
/datum/foo {
	var/string/asdf = "fdsa";
}

/proc/Example -> void {
	var/string/X = nameof(/proc); //"proc"
	X = nameof(/datum/foo/proc/Example); //"Example"
	X = nameof(asdf); //"asdf"
}

## Access Modifiers

### Static Procs

These compile to global procs

```dm
static /datum/test/proc/Foo() -> void {
    //src is not avaiable here
}

/world/New() -> void {
    //invoke as so
    /datum/test.Foo();
}
```

### Public, Protected, Readonly

All Typemaker accesses default to private. All DreamMaker access defaults to public

```dm
/proc/example() -> void {} //global procs public by default and cannot be decorated

/datum/test
{
    var/int/only_accessible_by_test = 1;
	readonly var/int/can_only_be_changed_in_constructor = 5;
    protected var/int/only_accessible_by_test_and_children = 2;
    public var/int/accessible_by_everyone = 3;
}

/datum/test/New() -> void {
	..();
	can_only_be_changed_in_constructor = 7;
}

public /datum/test/proc/ThisCanBeCalledByAnyone() -> void {}
protected /datum/test/proc/ThisCanOnlyBeCalledByTestOrChildren -> void {}
/datum/test/proc/ThisCanOnlyBeCalledByTest -> void {}
```

### Partial, Sealed

Datum definition block must occur before all proc definitions for said datum in a file

Datums with variable definitions in more than one block or proc definitions in more than one file must be declared as partial.

```dm
/datum/this_can_be_inherited {}
//procs for /datum/this_can_be_inherited cannot be defined before here

sealed /datum/this_can_be_inherited/but_this_cannot {}

sealed partial /datum/example_partial {
    var/int/i = 4;
}

partial /datum/example_partial {
    //sealed does not have to be redeclared
    var/int/j = 5;
}
```

### Virtual, Abstract, Final

Procs are no longer allowed to be considered virtual by default except in DreamMaker code

Abstract procs requires non-abstract child types to override the implementation.

Abstract can be applied at proc or datum level, both makes entirety of datum abstract. Abstract datums cannot be directly instantiated and their types cannot be used in `/path/concrete`

Datums can be sealed to prevent further inheritance

Arguments must be maintained by overrides. Default arguments must come last

Overridden procs may remove the `/nullable` spec from return types.

Virtual/abstract procs must be public or protected

`New()` is the only virtual proc that may have it's arguments changed by children

```dm
/datum/foo/proc/CannotBeOverridden() -> void {}

/datum/foo/New(int/first_arg) -> void {
	..()
}

protected virtual /datum/foo/proc/CanBeOverridden(datum/enforced_on_children) -> nullable/int {}

public abstract /datum/foo/proc/MustBeOverridden(int/x, datum/enforced_on_children = null) -> void;

/datum/foo/bar/New(string/can_have_different_args_than_parent) -> void {
	//but parent must still be called with correct args if at all
	if(prob(50))
		..(4);
}

/datum/foo/bar/CanBeOverridden() -> int {
    ..(); //not necessary
    return 4;
}

final /datum/foo/bar/MustBeOverridden(int/x = 4, datum/enforced_on_children = new) -> void {
    //cannot be overridden again
}
```

## Interfaces

`/interface` is a declarative only type that describes a set of public variables and procs a non-abstract datum must implement. Datums that implement interfaces are implicitly castable to interface vars of that type. 

`/interface` paths cannot be cast to `/path/concrete`

`var/interface/x;` is an invalid variable declaration.

`implements` must be in a declaration block of a datum to bind it to the contract

```dm
/interface/IEmptyInterfacesAreValidAndStillTypeChecked {}

/interface/IExample
{
    var/string/must_have_this_public_var;
    proc/MustHaveThisPublicProcWithThisSignature(int/x) -> void;
}

/datum/example {
    implements IExample;
    var/string/must_have_this_public_var;
}

/proc/InterfaceParameterAcceptanceExample(nullable/interface/IExample) -> void {}

public /datum/foo/bar/proc/MustHaveThisPublicProcWithThisSignature(int/x) -> void {
    InterfaceParameterAcceptanceExample(null);
    InterfaceParameterAcceptanceExample(src);
}

abstract /datum/foo {
    implements IExample;
    implements IEmptyInterfacesAreValidAndStillTypeChecked

    //abstract datums don't need to implement entire/any of interfaces
    var/string/must_have_this_public_var;
}

/datum/foo/bar {
    //cannot redeclare inherited implements
}

//virtual/abstract allowed
public virtual /datum/foo/bar/proc/MustHaveThisPublicProcWithThisSignature(int/x) -> void {}
```

## Order-Free Compilation

Remove macros entirely, hide quirks that make code inclusion order matter. Transpiled macros will be uniquely named to prevent namespace pollution

### True Const Variables

`var/const/*/Varname` optimized to `#define _<UNIQUE>_Varname` at the "cost" of removing them from `/datum.vars`. Still scoped appropriately

### Macro Inlining

The `inline` decorator marks a function to be compiled into wherever it is called. All procs may be inline. An `inline` datum makes all it's functions inline and implements variables (if any) as a `list()` in generated code.

```dm

inline /datum/gas_mixture/proc/assert_gas(path/gas_path) -> void {
    //src is valid

    //you know where i'm going with this
}

//globals of course can be inline too
inline /proc/BoldAnnounce() -> void {
    world << "This runs inline wherever it's called";
}
```

### Override Precedence

All functions have an override precedence which defaults to zero

When overriding the same proc more than once or decalaring and overriding the same proc in a datum, execution order is determined via override precedence from highest to lowest

It is a compilation error 2 or more of the same overrides in one path have the same precedence

Override precedence can be set with the `precedence()` decorator

Only virtual/abstract functions can have precedence like this

```dm

precedence(-1) /datum/foo/Bar() -> void {
    //this will not be called
}

virtual precedence(1) /datum/foo/proc/Bar() -> void {
    //this will be run 1st when called
    ..()
}

//precedence(0)
/datum/foo/Bar() -> void {
    //this will be run second when called
}
```

## .dmm Inclusion

`.dmm` files are now included via the top level `map()` directive

```dm
map('_maps/BoxStation/BoxStation.dmm')
```

## DreamMaker Compatibility

Option to include a `.dme` which will be the prefix for the output `.dme` the compiler genenerates.

If done, the `unsafe` block is unlocked to allow assigning from and calling into DM written code

`arglist()` and `call()()` cannot be used outside of unsafe blocks

```dm
/proc/dm_access_example() -> void {
    var/int/test
    var/int/test2
    unsafe {
        //typechecking stopped for this block
        var/datum/dm_declared_datum/D = new
        test = D.Func()
    }
    
    //test now assumed to be valid
    //test2 still unassigned
}
```

### Declarations

Declarations allow strong typing of existing DM types/var/functions without defining their values or bodies. These are used to expose the DM standard library to Typemaker code. Static and non-virtual procs cannot be declared. Untyped declarations cannot be used outside of `unsafe` blocks

foo.dm
```dm
/proc/Something() 
	world.log << "Hello world";

/datum/foo/var/whatever = list();
/datum/foo/var/whatever2 = "asdf";

/datum/foo/proc/Run()
	return 4
```dm

foo.tm
```dm
declare /proc/Something() -> void;

declare /datum/foo {
	public var/unknown_type_can_only_be_used_in_unsafe_block;

	//only public and protected allowed
	protected var/string/whatever2;
	//whatever can't be accessed by typemaker
	public /proc/Run() -> int;
}
```

bar.tm
```
/proc/bar() -> void {
	var/datum/foo = new;
	foo.Run();

	var/string/val;
	unsafe {
		val = foo.unknown_type_can_only_be_used_in_unsafe_block;
		foo.unknown_type_can_only_be_used_in_unsafe_block = 42;
	}
}
```

## Optimization

Transpiled code will use `:` access operators wherever possible. Code transpiled as relatively pathed for compiler optimization. Unreferenced code will be eliminated (`.vars` usage does not prevent this)

### Explicit keyword

The explicit keyword keeps variables/datum/functions from undergoing dead code elimination. Use this when these are valid reflection types

```dm
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

```

## Cross platform, easy installation

Single binary installer, ideally workable via shell command ala rust. Installs to ~/.typemaker/bin, multiple .so/.dlls fine provided no system dependencies required. Overwrite for updates. Delete to uninstall.

Compiler named `tmc`

### BYOND Version Management

Projects specify exactly which BYOND version to use for compilation in `typemaker.json` file. `tmc` handles downloading and installing versions in `~/.typemaker/byond` as necessary.

## Language Server and Linting

The `tm_langserv` and `tm_lint` binaries are to be included in the install bin folder

`tm_langserv` is a [langserver protocol](https://langserver.org/) executable

`tm_lint` automatically formats `.tm` files

## Folder Based Compilation

Automatically include all `.tm` files in a project root

# `typemaker.json` Format

```json
{
    "version": "<file schema semver>",
    "code_root": "<optionally specify a directory other than `.`>",
    "output_directory": "<optionally specify a directory other than `.`>",
    "byond_version": {
        "major": "<511/512/etc...>",
        "minor": "<1443/1444/etc...>"
    },
    "debug": "<true/false, default true, sets the DEBUG preprocessor directive>",
    "scripts":
    {
        "pre_transpile": "<Shell command to invoke before beginning transpilation>",
        "pre_compile": "<Shell command to invoke before running dm>",
        "post_compile": "<Shell command to invoke after successfully running dm>"
    },
    "dme": "<Path to .dme to #include before transpiled code>",
    "linter_settings": {
        "enforce_tabs": "<optional true/false, false enforces spaces, default non-enforce>",
        "allman_braces": "<optional true/false, false enforces BSD KNF, default non-enforce>",
        "no_operator_overloading": "<true/false, default true>",
        "no_single_line_blocks": "<true/false, default false>",
        "other_options": "to come"
    }
}
```

## Shell Command

```json
{
    windows: "<Path to .bat or ps1 file>",
    linux: "<Path to .sh file>"
}
```

# Feedback

Please leave feedback [here](https://github.com/Cyberboss/typemaker/issues/1)

parser grammar TypemakerParser;

options { tokenVocab=TypemakerLexer; }

compilation_unit: global_var_declaration* var_definition_statement* generic_declaration* global_proc_declaration* global_proc* datum_declaration*  datum_def* datum_proc* EOF;

proc_declaration: SLASH proc_type? proc_definition SEMI;
global_proc_declaration: DECLARE proc_declaration;
global_var_declaration: DECLARE var_definition_only SEMI;

datum_var_declaration: untyped_var_definition_only SEMI | var_decorations untyped_var_definition_only SEMI;
datum_declaration_item: datum_var_declaration | set_assignment_statement | proc_decorator_set proc_interface | implements_statement;
datum_declaration_items: datum_declaration_item+;
datum_declaration_block: LCURL RCURL | LCURL datum_declaration_items RCURL;
datum_declaration: DECLARE datum_decorator_set fully_extended_identifier datum_declaration_block;

generic_declaration: enum | interface;

map: MAP LPAREN RES RPAREN SEMI;

number: INTEGER | REAL | MINUS INTEGER | MINUS REAL;

enum_type: SLASH ENUM SLASH IDENTIFIER;

concrete_path: PATH SLASH CONCRETE;
path_type: concrete_path | PATH;

string_content: CHAR_INSIDE | STRING_INSIDE | MULTI_STRING_INSIDE;
string_body: string_content+ | string_content+ EMBED_START expression RBRACE string_body | EMBED_START expression RBRACE string_content+ | EMBED_START expression RBRACE;

multi_string: MULTI_STRING_START string_body MULTI_STRING_CLOSE;
line_string: STRING_START string_body STRING_CLOSE;
dynamic_string: line_string | multi_string;

const_string: VERBATIUM_STRING | MULTILINE_VERBATIUM_STRING;
string
	: const_string 
	| dynamic_string
	;

dict_type: DICT SLASH nullable_type BSLASH nullable_type;
root_type: enum_type | path_type | interface_type | dict_type | INT | RESOURCE | BOOL | FLOAT | EXCEPTION;
list_identifier: IDENTIFIER | LIST;
extended_identifier: IDENTIFIER | IDENTIFIER fully_extended_identifier;
fully_extended_identifier: SLASH extended_identifier;

true_type: root_type | extended_identifier | LIST SLASH nullable_type;
nullable_type: true_type | NULLABLE SLASH true_type;
const_type: true_type | CONST SLASH true_type;
type: const_type | nullable_type;
return_type: nullable_type | VOID | IDENTIFIER;

typed_identifier: type SLASH IDENTIFIER;
untyped_identifier: typed_identifier | IDENTIFIER;

var_definition_only: VAR SLASH typed_identifier;
untyped_var_definition_only: VAR SLASH untyped_identifier;
var_definition: var_definition_only | var_definition_only EQUALS expression;
var_definition_statement: var_definition SEMI;

inc_dec: INC | DEC;

dict_declaration: string EQUALS expression;
dict_declarations: dict_declaration | dict_declaration COMMA dict_declarations;
dict_definition: DICT LPAREN RPAREN | DICT LPAREN dict_declarations RPAREN;

list_declarations: expression | expression COMMA list_declarations;
list_definition: LIST LPAREN RPAREN | LIST LPAREN list_declarations RPAREN;

nameof_expression: NAMEOF LPAREN target RPAREN;

new_type: true_type | accessed_target | DOT;

new_expression
	: NEW new_type
	| NEW new_type argument_list
	| NEW argument_list
	| NEW
	;

input_interior
	: expression COMMA expression COMMA expression
	| expression COMMA expression COMMA expression COMMA expression
	;

input_expression
	: input_start AS nullable_type IN expression
	| input_start AS nullable_type
	| input_start IN expression
	| input_start
	;

input_start: INPUT LPAREN input_interior RPAREN;

accessor
	: nonoptional_accessor
	| QUESTION nonoptional_accessor
	;

nonoptional_accessor
	: DOT	// x.y
	| COLON	// x:y
	;

accessed_target
	: accessed_target accessor IDENTIFIER
	| IDENTIFIER
	;

in_expression: target IN expression;

expression
	: target
	| assignment
	| EXCLAIM expression
	| expression POW expression
	| expression STAR expression
	| expression SLASH expression
	| expression PLUS expression 
	| expression MINUS expression
	| expression BAND expression
	| expression BOR expression
	| expression XOR expression
	| expression LSHIFT expression
	| expression RSHIFT expression
	| INVERT expression
	| MINUS expression
	| inc_dec expression
	| expression inc_dec
	| expression LAND expression
	| expression LOR expression
	| expression LESS expression
	| expression GREATER expression
	| expression LESSE expression
	| expression GREATERE expression
	| expression EEQUALS expression
	| expression NEQUALS expression
	| expression QUESTION expression COLON expression
	| new_expression
	| input_expression
	;

invocation
	: call_invocation
	| basic_identifier argument_list
	;

target
	: bracketed_expression
	| fully_extended_identifier
	| list_definition
	| dict_definition
	| nameof_expression	
	| invocation
	| target accessor invocation
	| target accessor IDENTIFIER
	| target LBRACE expression RBRACE	//list access
	| string
	| basic_identifier
	| fully_extended_identifier
	| path_type
	| RES
	| TRUE
	| FALSE
	| INT
	| FLOAT
	| NULL
	;

basic_identifier
	: IDENTIFIER
	| DOTDOT
	| GLOBAL
	| SRC
	| USR
	| DOT
	;

assignment
	: target OEQUALS expression
	| target AEQUALS expression
	| target PEQUALS expression
	| target MEQUALS expression
	| target TEQUALS expression
	| target SEQUALS expression
	| target IEQUALS expression
	| target DEQUALS expression
	| target LEQUALS expression
	| target REQUALS expression
	| target EQUALS expression;

target_var: target | var_definition_only;

argument: expression | basic_identifier EQUALS expression;
arguments: argument | argument arguments;
argument_list: LPAREN RPAREN | LPAREN arguments RPAREN;

call_invocation: CALL argument_list | CALL LPAREN string RPAREN argument_list;

in_for: LPAREN target_var IN expression RPAREN;

for_var_def: target_var SEMI | SEMI;
for_condition: expression SEMI | SEMI;
for_statement: semicolonless_statement RPAREN | RPAREN;
standard_for: LPAREN for_var_def for_condition for_statement;

for_type: standard_for | in_for;
for: FOR for_type statement_block;

return_statement: RETURN expression SEMI | RETURN SEMI;

bracketed_expression: LPAREN expression RPAREN;

while_params: WHILE bracketed_expression;
while: while_params statement_block;
do_while: DO statement_block while_params SEMI;

cases: if_start | else;
switch_block: LCURL RCURL | LCURL cases RCURL;
switch: SWITCH bracketed_expression switch_block;

else: ELSE statement_block;
elseif: ELSE if_start;
if_continuation: elseif if_continuation | elseif | else;
if_start: IF bracketed_expression statement_block;
if: if_start if_continuation | if_start;

flow_control: return_statement | while | do_while | for | switch | if;

identifier_assignment: basic_identifier EQUALS expression SEMI;
set_assignment_statement: SET identifier_assignment;
set_statement: set_assignment_statement | SET in_expression SEMI;

unsafe_block: UNSAFE unsafe_statement | UNSAFE LCURL unsafe_statement+ RCURL;
target_shift: expression LSHIFT expression | expression RSHIFT expression;

spawn_block: SPAWN LPAREN number RPAREN statement_block;

try_block: TRY statement_block CATCH LPAREN EXCEPTION SLASH IDENTIFIER RPAREN statement_block;

semicolonless_statement: target_shift | assignment | invocation | BREAK | CONTINUE;
unsafe_statement: var_definition_statement | set_statement | return_statement | flow_control | try_block | semicolonless_statement SEMI;
safe_statement: unsafe_block | unsafe_statement;
statement_block: safe_statement | LCURL safe_statement+ RCURL;
proc_block: statement_block | unsafe_block;

var_decorations: access | access READONLY | READONLY | STATIC READONLY | access STATIC READONLY | access STATIC | STATIC;
decorated_var_definition_statement: var_decorations var_definition_statement | var_definition_statement | identifier_assignment;
decorated_var_definition_statements: decorated_var_definition_statement+;

datum_decorator: universal_decorator | SEALED | PARTIAL;
datum_decorator_set: datum_decorator+ SLASH | SLASH;
implements_statement: IMPLEMENTS IDENTIFIER SEMI;
datum_block_interior: implements_statement+ decorated_var_definition_statements | implements_statement+ | decorated_var_definition_statements;
datum_block: LCURL datum_block_interior RCURL | LCURL RCURL;
datum_def: datum_decorator_set extended_identifier datum_block;

argument_declaration: untyped_identifier | untyped_identifier EQUALS expression;
argument_declaration_list: argument_declaration | argument_declaration COMMA argument_declaration_list | VARARGS;

proc_arguments: LPAREN RPAREN | LPAREN argument_declaration_list RPAREN;
proc_identifier: IDENTIFIER | CONSTRUCTOR;
proc_name_and_args: SLASH proc_identifier proc_arguments;
proc_definition: proc_name_and_args RDEC return_type | proc_name_and_args;

access: PUBLIC | PROTECTED;
precedence: PRECEDENCE LPAREN INTEGER RPAREN;

universal_decorator: EXPLICIT | INLINE | ABSTRACT;
proc_decorator: precedence | access | universal_decorator | VIRTUAL | FINAL | STATIC;

proc_decorator_set: proc_decorator+ SLASH | SLASH;

global_proc_decorator_set: universal_decorator+ SLASH | SLASH;

global_proc: global_proc_decorator_set proc_type proc_definition proc_block;

proc_type: PROC | VERB;
datum_proc: proc_decorator_set extended_identifier proc_type? proc_definition proc_block;

interface_type: INTERFACE SLASH IDENTIFIER;

proc_interface: PROC proc_definition SEMI;
interface_definition: var_definition_only SEMI | proc_interface;
interface_block: LCURL RCURL | LCURL interface_definition+ RCURL;
interface: SLASH interface_type interface_block;

enum_value: INTEGER | const_string;
enum_definition: IDENTIFIER EQUALS enum_value | IDENTIFIER;
enum_definitions: enum_definition | enum_definition COMMA enum_definitions;
enum_block: LCURL RCURL | LCURL enum_definitions RCURL;
enum: enum_type enum_block;

parser grammar TypemakerParser;

options { tokenVocab=TypemakerLexer; }

compilation_unit: typemaker_file EOF | map_file EOF | declaration_file EOF;

map_file: map+;

proc_declaration: SLASH PROC proc_definition SEMI;
global_proc_declaration: DECLARE proc_declaration;

datum_declaration_item: var_definition_only SEMI | proc_decorator_set proc_interface;
datum_declaration_items: datum_declaration_item | datum_declaration_item datum_declaration_items;
datum_declaration_block: LCURL RCURL | LCURL datum_declaration_items RCURL;
datum_declaration: DECLARE fully_extended_identifier datum_declaration_block;
datum_declaration_file: datum_declaration+;

global_declaration_file: global_var* global_proc_declaration*;

declaration_file: global_declaration_file | datum_declaration_file;

typemaker_file: datum_file | globals_file;

globals_file: global_var* generic_file global_proc*;
datum_file: generic_file datum_def+ datum_proc*;
generic_file: enum* interface*;

map: MAP LPAREN RES RPAREN SEMI;

number: INTEGER | REAL;

enum_type: ENUM SLASH IDENTIFIER;

concrete_path: PATH SLASH CONCRETE;
path_type: concrete_path | PATH;

/* //TODO: embedded expressions

string_content: CHAR_INSIDE | STRING_INSIDE;
string_body: string_content | string_content string_body;

multi_string: MULTI_STRING_START string_body MULTI_STRING_CLOSE;
line_string: STRING_START string_body STRING_CLOSE;
dynamic_string: line_string | multi_string;
*/
const_string: VERBATIUM_STRING | MULTILINE_VERBATIUM_STRING;
string
	: const_string 
	//TODO: embedded expressions
	//| dynamic_string
	;

root_type: enum_type | path_type | interface_type | LIST | INT | FILE | RESOURCE | BOOL | FLOAT | EXCEPTION;
list_identifier: IDENTIFIER | LIST;
extended_list_type:  SLASH list_identifier | list_identifier SLASH extended_list_type;
extended_identifier: IDENTIFIER | IDENTIFIER fully_extended_identifier;
fully_extended_identifier: SLASH extended_identifier;

true_type: root_type | extended_identifier | LIST extended_list_type;
type: true_type | NULLABLE SLASH true_type;
return_type: type | VOID;

typed_identifier: type SLASH IDENTIFIER;

var_definition_only: VAR SLASH typed_identifier;
var_definition: var_definition_only | var_definition_only EQUALS expression;
var_definition_statement: var_definition SEMI;

inc_dec: INC | DEC;

list_declaration: string EQUALS expression | expression;
list_declarations: list_declaration | list_declaration COMMA list_declarations;
list_declaration_list: LPAREN RPAREN | LPAREN list_declarations RPAREN;
list_definition: LIST list_declaration_list;

nameof_expression: NAMEOF LPAREN target RPAREN;

new_expression: NEW true_type | NEW true_type argument_list | NEW argument_list | NEW;

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
	| expression PUSH expression
	| expression PULL expression
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
	| expression IN expression
	| expression QUESTION expression COLON expression
	| new_expression
	| path_type
	| RESOURCE
	| TRUE
	| FALSE
	| INT
	| FLOAT
	| NULL
	;

invocation
	: call_invocation
	| basic_identifier argument_list
	;

target
	: bracketed_expression
	| fully_extended_identifier
	| list_definition
	| nameof_expression	
	| invocation
	| target accessor invocation
	| target accessor IDENTIFIER
	| target LBRACE expression RBRACE	//list access
	| string
	| basic_identifier
	;

accessor
	: DOT	// x.y
	| QUESTION DOT	// x?.y
	| COLON	// x:y
	| QUESTION COLON	// x?:y
	;

basic_identifier
	: IDENTIFIER
	| DOTDOT
	| SRC
	| DOT
	;

assignment
	: target OEQUALS expression
	| target AEQUALS expression
	| target PEQUALS expression
	| target MEQUALS expression
	| target TEQUALS expression
	| target SEQUALS expression
	| target EQUALS expression;

target_var: target | var_definition_only;

argument: expression | IDENTIFIER EQUALS expression;
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

identifier_assignment: IDENTIFIER EQUALS expression SEMI;
set_statement: SET identifier_assignment;

unsafe_block: UNSAFE statement_block;
push_pull: expression PUSH expression | expression PULL expression;

try_block: TRY statement_block CATCH LPAREN EXCEPTION SLASH IDENTIFIER RPAREN statement_block;

semicolonless_statement: push_pull | assignment | invocation | BREAK | CONTINUE;
statement: var_definition_statement | set_statement | return_statement | flow_control | unsafe_block | try_block | semicolonless_statement SEMI;
statements: statement | statement statements;
statement_block: statement | LCURL statements RCURL;
proc_block: statement_block | unsafe_block;

var_decorations: access | access READONLY | READONLY;
decorated_var_definition_statement: var_decorations var_definition_statement | var_definition_statement | identifier_assignment;
decorated_var_definition_statements: decorated_var_definition_statement | decorated_var_definition_statement decorated_var_definition_statements;

datum_decorator: universal_decorator | SEALED | PARTIAL;
datum_decorators: datum_decorator | datum_decorator datum_decorators;
datum_decorator_set: datum_decorators SLASH | SLASH;
datum_block: LCURL decorated_var_definition_statements RCURL | LCURL RCURL;
datum_def: datum_decorator_set extended_identifier datum_block;

argument_declaration: typed_identifier | typed_identifier EQUALS expression;
argument_declaration_list: argument_declaration | argument_declaration COMMA argument_declaration_list;

proc_arguments: LPAREN RPAREN | LPAREN argument_declaration_list RPAREN;
proc_definition: SLASH IDENTIFIER proc_arguments RDEC return_type;

access: PUBLIC | PROTECTED;
precedence: PRECEDENCE LPAREN INTEGER RPAREN;

universal_decorator: EXPLICIT | INLINE | ABSTRACT;
proc_decorator: precedence | access | universal_decorator | VIRTUAL | FINAL | STATIC;

proc_decorators: proc_decorator | proc_decorator proc_decorators;
proc_decorator_set: proc_decorators SLASH | SLASH;

global_proc_decorators: universal_decorator | universal_decorator global_proc_decorators;
global_proc_decorator_set: global_proc_decorators SLASH | SLASH;

global_proc: global_proc_decorator_set PROC proc_definition proc_block;

datum_proc_type: PROC | VERB;
datum_proc: proc_decorator_set extended_identifier datum_proc_type proc_definition proc_block;

interface_type: INTERFACE fully_extended_identifier;

proc_interface: PROC proc_definition SEMI;
interface_definition: var_definition_only SEMI | proc_interface;
interface_definitions: interface_definition | interface_definition interface_definitions;
interface_block: LCURL RCURL | LCURL interface_definitions RCURL;
interface: SLASH interface_type interface_block;

enum_value: INTEGER | const_string;
enum_definition: IDENTIFIER EQUALS enum_value | IDENTIFIER;
enum_definitions: enum_definition | enum_definition COMMA enum_definitions;
enum_block: LCURL RCURL | LCURL enum_definitions RCURL;
enum: enum_type enum_block;

global_var: var_definition_statement;

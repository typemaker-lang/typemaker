grammar Typemaker;

/*
 * Lexer Rules
 */

NUMBER: '0'..'9';

BLOCKCOMMENT: '/*' .*? '*/' -> skip;
LINECOMMENT: '//' ~[\r\n]* -> skip;

INC: '++';
DEC: '--';
PEQUALS: '+=';
MEQUALS: '-=';
EEQUALS: '==';
NEQUALS: '!=';
TEQUALS: '*=';
SEQUALS: '/=';
AEQUALS: '&=';
COMMA: ',';
OEQUALS: '|=';
RDEC: '->';
PLUS: '+';
MINUS: '-';
POWER: '**';
STAR: '*';
XOR: '^';
LPARENT: '(';
RPAREN: ')';
LCURL: '{';
RCURL: '}';
LOR: '||';
LAND: '&&';
BOR: '|';
BAND: '&';
LBRACE: '[';
RBRACE: ']';
DOT: '.';
COLON: ':';
SEMI: ';';
EQUALS: '=';
QUESTION: '?';
EXCLAIM: '!';
AT: '@';
BSLASH: '\\';

RES: '\'' .+? '\'';


NEW: 'new';
PULL: '<<';
PUSH: '>>';
LESSE: '<=';
GREATERE: '>=';
LESS: '<';
GREATER: '>';
TRUE: 'true';
FALSE: 'false';
LIST: 'list';
CONST: 'const';
ENUM: 'enum';
FOR: 'for';
WHILE: 'while';
IN: 'in';
TO: 'to';
IF: 'if';
ELSE: 'else';
SWITCH: 'switch';
BREAK: 'break';
CONTINUE: 'continue';
RETURN: 'return';
DO: 'do';
AS: 'as';
STATIC: 'static';
VAR: 'var';
VOID: 'void';
EXPLICIT: 'explicit';

PUBLIC: 'public';
PROTECTED: 'protected';
VIRTUAL: 'virtual';
SEALED: 'sealed';
PRECEDENCE: 'precedence';
FINAL: 'final';
ABSTRACT: 'abstract';
INLINE: 'inline';
SLASH: '/';
CONCRETE: 'concrete';
FILE: 'file';
RESOURCE: 'resource';
BOOL: 'bool';
STRING: 'string';
PATH: 'path';
INT: 'int';
FLOAT: 'float';
INTERFACE: 'interface';
NULLABLE: 'nullable';
NULL: 'null';
PROC: 'proc';

CALL: 'call';
SRC: 'src';

EXCEPTION: 'exception';
VERB: 'verb';
SET: 'set';

MAP: 'map';

IDENTIFIER: [_a-zA-Z][_a-zA-Z0-9]*;

NEWLINE: [\n] -> channel(HIDDEN);
WS: [ \t\r]+ -> channel(HIDDEN);

typemakerFile: map | global_proc | datum_def | datum_proc | enum | interface | EOF;

map: MAP LPAREN RES RPAREN SEMI;

global_proc: proc_decorator_set PROC SLASH proc_definition;

integer: MINUS NUMBER | NUMBER;

enum_type: ENUM SLASH IDENTIFIER;

concrete_path: PATH SLASH CONCRETE;
path_type: concrete_path | PATH;

root_type: enum_type | path_type | LIST | INTERFACE | INT | FILE | RESOURCE | BOOL | FLOAT;
list_identifier: IDENTIFIER | LIST;
extended_list_type: list_identifier | list_identifier SLASH extended_list_type;
extended_identifier: IDENTIFIER | IDENTIFIER SLASH extended_identifier;

true_type: root_type | extended_identifier | LIST SLASH extended_list_type;
type: true_type | NULLABLE SLASH true_type;
return_type: type | VOID;

typed_identifier: type SLASH IDENTIFIER;

var_definition_statement: VAR SLASH typed_identifier | VAR SLASH typed_identifier EQUALS expression SEMI;

inc_dec: INC | DEC;

math_expression
	: expression PLUS expression 
	| expression MINUS expression
	| expression SLASH expression
	| expression POW expression
	| expression STAR expression
	| expression BAND expression
	| expression BOR expression
	| expression XOR expression
	| expression PUSH expression
	| expression PULL expression
	| inc_dec math_expression
	| math_expression inc_dec
	| INT
	| FLOAT;

logical_expression
	: expression LAND expression
	| expression LOR expression
	| expression LESS expression
	| expression GREATER expression
	| expression LESSE expression
	| expression GREATERE expression
	| expression EEQUALS expression
	| expression NEQUALS expression
	| EXCLAIM expression
	| TRUE
	| FALSE;

list_declaration: string EQUALS expression | expression;
list_declarations: list_declaration | list_declaration COMMA list_declarations;
list_declaration_list: LPAREN RPAREN | LPAREN list_declarations RPAREN;
list: LIST list_declaration_list;

expression: invocation | assignment | list | LPAREN expression RPAREN | logical_expression | math_expression | string | RESOURCE | NULL;

assignment
	: target OEQUALS expression
	| target AEQUALS expression
	| target PEQUALS expression
	| target MEQUALS expression
	| target TEQUALS expression
	| target SEQUALS expression
	| target EQUALS expression;

valid_calls: FILE | IDENTIFIER;
target: expression DOT IDENTIFIER | valid_calls;

argument: expression | IDENTIFIER EQUALS expression;
arguments: argument | argument arguments;
argument_list: LPAREN RPAREN | LPAREN arguments RPAREN;

call_invocation: CALL argument_list | CALL LPAREN string RPAREN argument_list;
invocation: target argument_list | call_invocation;

return_statement: RETURN expression SEMI | RETURN SEMI; 

flow_control: return_statement | while | do_while | for | switch | if | BREAK SEMI | CONTINUE SEMI;

set_statement: SET IDENTIFIER EQUALS expression SEMI;

push_pull: expression PUSH expression | expression PULL expression;

statement: var_definition_statement | set_statement | flow_control | push_pull SEMI | assignment SEMI | invocation SEMI;
statements: statement | statement statements;
statement_block: statement | LCURL statements RCURL;

access_var_definition_statement: access var_definition_statement | var_definition_statement;
access_var_definition_statements: access_var_definition_statement | access_var_definition_statement access_var_definition_statements;

datum_block: LCURL access_var_definition_statements RCURL | LCURL RCURL;
root_identifier: root_datum SLASH extended_identifier | root_datum;
datum_def: datum_decorator_set root_datum root_identifier datum_block;

argument_declaration: typed_identifier | typed_identifier EQUALS expression;
argument_declaration_list: argument_declaration | argument_declaration COMMA argument_declaration_list;

proc_arguments: LPAREN RPAREN | LPAREN argument_declaration_list RPAREN;
proc_definition: IDENTIFIER proc_arguments RDEC return_type statement_block;

access: PUBLIC | PROTECTED;
precedence: PRECEDENCE LPAREN integer RPAREN;

proc_decorator: precedence | access | EXPLICIT | INLINE | VIRTUAL | ABSTRACT | FINAL | STATIC;
proc_decorators: proc_decorator | proc_decorator proc_decorators;
proc_decorator_set: proc_decorators SLASH | SLASH;

enum_value: integer | const_string;
enum_definition: IDENTIFIER EQUALS enum_value | IDENTIFIER;
enum_definitions: enum_definition | enum_definition COMMA enum_definitions;
enum_block: LCURL RCURL | LCURL enum_definitions RCURL;
enum: enum_type enum_block;

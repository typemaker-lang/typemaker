grammar Typemaker;

/*
 * Parser Rules
 */



compileUnit
	:	EOF
	;

/*
 * Lexer Rules
 */

NUMBER: '0'..'9';

OPENC: '/*';
CLOSEC: '*/';
INC: '++';
DEC: '--';
PEQUALS: '+=';
MEQUALS: '-=';
EEQUALS: '==';
TEQUALS: '*=';
SEQUALS: '/=';
AEQUALS: '&=';
OEQUALS: '|=';
RDEC: '->';
PLUS: '+';
MINUS: '-';
POWER: '**';
STAR: '*';
XOR: '^';
LPARENT: '(';
RPAREN: ')';
OPENSTR: '{"';
CLOSESTR: '"}';
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
SQUOTE: '\'';
DQUOTE: '"';
NEW: 'new';
PULL: '<<';
PUSH: '>>';
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
DSLASH: '//';
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
DATUM: 'datum';
EXCEPTION: 'exception';
VERB: 'verb';
SET: 'set';
WORLD: 'world';

IDENTIFIER: [a-zA-Z][a-zA-Z0-9]*;

WS: [ \n\t\r]+ -> skip;

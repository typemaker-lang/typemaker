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


BLOCKCOMMENT: '/*' .*? '*/' -> skip;
LINECOMMENT: '//' ~[\r\n]* -> skip;

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

RES: '\'' .+? '\'';
LSTRING: '"' ('\\[' | ~[[\r\n'])* '[';
RSTRING: ']' ('\\"' | ~["\r\n])* '"'; 
CSTRING: ']' ('\\[' | ~[[\r\n])* '['; 
FSTRING: '"' ('\\"' | ~["\r\n])* '"';

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
DATUM: 'datum';
EXCEPTION: 'exception';
VERB: 'verb';
SET: 'set';
WORLD: 'world';

IDENTIFIER: [_a-zA-Z][_a-zA-Z0-9]*;

NEWLINE: [\n] -> channel(HIDDEN);
WS: [ \t\r]+ -> channel(HIDDEN);

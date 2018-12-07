lexer grammar TypemakerLexer;

//a lot of this was stolen from here: https://github.com/antlr/grammars-v4/blob/edc36d1/csharp/CSharpLexer.g4

channels { COMMENTS_CHANNEL }

SINGLE_LINE_COMMENT:     '//'  InputCharacter*    -> channel(COMMENTS_CHANNEL);
DELIMITED_COMMENT: '/*' .*? '*/' -> channel(COMMENTS_CHANNEL);

fragment InputCharacter: ~[\r\n\u0085\u2028\u2029];
fragment NEWLINE
	: '\u000D' //'<Carriage Return CHARACTER (U+000D)>'
	| '\u000A' //'<Line Feed CHARACTER (U+000A)>'
	| '\u0085' //'<Next Line CHARACTER (U+0085)>'
	| '\u2028' //'<Line Separator CHARACTER (U+2028)>'
	| '\u2029' //'<Paragraph Separator CHARACTER (U+2029)>'
	;
fragment WHITESPACE
	: UnicodeClassZS
	| '\u0009' //'<Horizontal Tab Character (U+0009)>'
	| '\u000B' //'<Vertical Tab Character (U+000B)>'
	| '\u000C' //'<Form Feed Character (U+000C)>'
	;
fragment UnicodeClassZS
	: '\u0020' // SPACE
	| '\u00A0' // NO_BREAK SPACE
	| '\u1680' // OGHAM SPACE MARK
	| '\u180E' // MONGOLIAN VOWEL SEPARATOR
	| '\u2000' // EN QUAD
	| '\u2001' // EM QUAD
	| '\u2002' // EN SPACE
	| '\u2003' // EM SPACE
	| '\u2004' // THREE_PER_EM SPACE
	| '\u2005' // FOUR_PER_EM SPACE
	| '\u2006' // SIX_PER_EM SPACE
	| '\u2008' // PUNCTUATION SPACE
	| '\u2009' // THIN SPACE
	| '\u200A' // HAIR SPACE
	| '\u202F' // NARROW NO_BREAK SPACE
	| '\u3000' // IDEOGRAPHIC SPACE
	| '\u205F' // MEDIUM MATHEMATICAL SPACE
;

WHITESPACES: (WHITESPACE | NEWLINE)+ -> channel(HIDDEN);

VERBATIUM_STRING: '@"' (~["\r\n])* '"';
MULTILINE_VERBATIUM_STRING: '@{"' (~'"')* '"}';

fragment CommonCharacter
	: '\\\''
	| '\\"'
	| '\\\\'
	| '\\0'
	| '\\a'
	| '\\b'
	| '\\f'
	| '\\n'
	| '\\r'
	| '\\t'
	| '\\v'
	;

INTEGER: ('0'..'9')+;
REAL: ('0'..'9')+ '.' ('0'..'9')+;

BLOCKCOMMENT: '/*' .*? '*/' -> skip;
LINECOMMENT: '//' ~[\r\n]* -> skip;

CONSTRUCTOR: 'New';
VARARGS: '...';
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
POW: '**';
STAR: '*';
XOR: '^';
LPAREN: '(';
RPAREN: ')';
LCURL: '{';
RCURL: '}';
LOR: '||';
LAND: '&&';
BOR: '|';
BAND: '&';
LBRACE: '[';
RBRACE: ']';
DOTDOT: '..';
DOT: '.';
COLON: ':';
SEMI: ';';
EQUALS: '=';
QUESTION: '?';
EXCLAIM: '!';
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
PARTIAL: 'partial';
READONLY: 'readonly';
NAMEOF: 'nameof';
IMPLEMENTS: 'implements';

SPAWN: 'spawn';

TRY: 'try';
CATCH: 'catch';

DECLARE: 'declare';
UNSAFE: 'unsafe';

SLASH: '/';

TM_BASE: '__typemaker_base_object';

CONCRETE: 'concrete';
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
DICT: 'dict';

CALL: 'call';
SRC: 'src';

EXCEPTION: 'exception';
VERB: 'verb';
SET: 'set';

MAP: 'map';

IDENTIFIER: [_a-zA-Z][_a-zA-Z0-9]*;

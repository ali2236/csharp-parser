# C# Parser

### Grammar:
```javascript
<statements> = <statement> <statement> | $
<statement> = <expression>;
            | if(<expression>)<statement>
            | if(<expression>) else <statement>
            | for(<optional expression>;<optional expression>;<optional expression>)<statement>
            | while(<expression>)<statement>
            | do<statement>while(<expression>);
            | <id><assignment><expression>
            | <id> <id> = <expression>
            | {<statements>}

<call> = <id>(<optional params>)
<optional params> = <params> | $
<params> = <expression>,<expression> | <expression>

<optional expression> = <expression> | $
<expression> = <call> 
            | <id>
            | <id>++
            | <number>
            | <expression><relation><expression>
            | <expression><binary operator><expression>

<relation> = == | != | > | >= | < | <=
<binary operator> = + | - | * | /
```
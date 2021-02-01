using System;
using System.Collections.Generic;
using System.Linq;

namespace csharpAnalyzer {
    public class Parser {

        #region helpers
        private CsharpDictionary Dictionary = new CsharpDictionary();
        private IList<Token> tokens { get; }

        private int index = 0;
        private Token lookahead => tokens[index];

        public Parser(IEnumerable<Token> tokens) {
            this.tokens = tokens.ToList();
        }

        private Parser Match(Token token) {
            if (Equals(lookahead, token)) index++;
            else throw new Exception($"Syntax Error. Expected {token}, found {lookahead} at index {index}.");
            return this;
        }

        private Parser Match(String ks) {
            Match(Dictionary.Identify(ks));
            return this;
        }

        private Parser Match(TokenType type) {
            if (lookahead.TokenType == type) index++;
            else throw new Exception($"Syntax Error. Expected {type}, found {lookahead} at index {index}.");
            return this;
        }

        private bool TokenIs(TokenType type) => lookahead.TokenType == type;
        private bool TokenIs(String s) => lookahead.TokenType == Dictionary.Identify(s).TokenType;
        #endregion

        public void parse() {
            Statements();
        }

        private void Statements() {
            while (index < tokens.Count
                   && lookahead.TokenType
                   != TokenType.ClosedBracket)
                Statement();
        }

        private Parser Statement() {
            switch (lookahead.TokenType) {
                case TokenType.If: // if [else]
                    Match("if").Match("(").Expression().Match(")")
                        .Statement().OptionalElse();
                    break;
                case TokenType.For: // for loop
                    Match("for").Match("(")
                        .OptionalStatement().Match(";")
                        .OptionalExpression().Match(";")
                        .OptionalExpression().Match(")")
                        .Statement();
                    break;
                case TokenType.While: // while loop
                    Match("while").Match("(").Expression().Match(")")
                        .Statement();
                    break;
                case TokenType.Do: // do while loop
                    Match("do")
                        .Statement()
                        .Match("while").Match("(").Expression().Match(")")
                        .Match(";");
                    break;
                case TokenType.Id: // assignment or deceleration
                    Match(TokenType.Id);
                    if (TokenIs(TokenType.Assignment)) Match(TokenType.Assignment).Expression();
                    else if(TokenIs(TokenType.Id)) Match(TokenType.Id).Match("=").Expression();
                    else if (TokenIs(TokenType.OpenParentheses)) Call();
                    break;
                case TokenType.OpenBracket: // statements
                    Match(TokenType.OpenBracket);
                    Statements();
                    Match(TokenType.ClosedBracket);
                    break;
                default:
                    Expression().Match(";");
                    break;
            }

            return this;
        }

        private Parser Call() {
            Match("(").OptionalParams().Match(")");
            return this;
        }

        private Parser OptionalParams() {
            if (!TokenIs(TokenType.ClosedParentheses)) Params();
            return this;
        }

        private Parser OptionalStatement() {
            if (!TokenIs(";")) Statement();
            return this;
        }

        private Parser Params() {
            Expression();
            while (TokenIs(TokenType.Comma)) Match(TokenType.Comma).Expression();
            return this;
        }


        private Parser OptionalElse() {
            if (TokenIs(TokenType.Else)) Match("else").Statement();
            return this;
        }


        private Parser OptionalExpression() {
            if (!TokenIs(TokenType.SemiColon)) Expression();
            return this;
        }

        private Parser Expression() {
            switch (lookahead.TokenType) {
                case TokenType.Id:
                    Match(TokenType.Id);
                    if (TokenIs("(")) Call();
                    else if (TokenIs("++")) Match("++");
                    else if (TokenIs(TokenType.Relation)) Expression();
                    break;
                case TokenType.Number:
                    Match(TokenType.Number);
                    break;
                case TokenType.BinaryOperator:
                    Match(TokenType.BinaryOperator).Expression();
                    break;
                case TokenType.Relation:
                    Match(TokenType.Relation).Expression();
                    break;
            }
            return this;
        }
    }
}
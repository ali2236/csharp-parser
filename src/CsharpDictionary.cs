using System.Collections.Generic;

namespace csharpAnalyzer
{
    public class CsharpDictionary
    {
        private IDictionary<string, Token> Reserved { get; }
        private void addKeyword(Token token) => Reserved[token.Attribute] = token;

        public CsharpDictionary()
        {
            Reserved = new Dictionary<string, Token>();
            // keywords
            //addKeyword(new Token(TokenType.True, "true"));
            //addKeyword(new Token(TokenType.False, "false"));
            addKeyword(new Token(TokenType.If, "if"));
            addKeyword(new Token(TokenType.Else, "else"));
            addKeyword(new Token(TokenType.For, "for"));
            addKeyword(new Token(TokenType.While, "while"));
            addKeyword(new Token(TokenType.Do, "do"));
            // symbols
            addKeyword(new Token(TokenType.Assignment, "="));
            addKeyword(new Token(TokenType.Assignment, "+="));
            addKeyword(new Token(TokenType.Assignment, "-="));
            addKeyword(new Token(TokenType.Assignment, "*="));
            addKeyword(new Token(TokenType.Assignment, "/="));
            addKeyword(new Token(TokenType.BinaryOperator, "+"));
            addKeyword(new Token(TokenType.BinaryOperator, "-"));
            addKeyword(new Token(TokenType.BinaryOperator, "*"));
            addKeyword(new Token(TokenType.BinaryOperator, "/"));
            addKeyword(new Token(TokenType.Relation, "=="));
            addKeyword(new Token(TokenType.Relation, "=>"));
            addKeyword(new Token(TokenType.Relation, "=<"));
            addKeyword(new Token(TokenType.Relation, "!="));
            addKeyword(new Token(TokenType.Relation, ">"));
            addKeyword(new Token(TokenType.Relation, "<"));
            addKeyword(new Token(TokenType.OpenBracket, "{"));
            addKeyword(new Token(TokenType.ClosedBracket, "}"));
            addKeyword(new Token(TokenType.OpenParentheses, "("));
            addKeyword(new Token(TokenType.ClosedParentheses, ")"));
            addKeyword(new Token(TokenType.SemiColon, ";"));
            addKeyword(new Token(TokenType.Comma, ","));
            addKeyword(new Token(TokenType.PlusPlus, "++"));
            
        }

        public Token Identify(string s) {
            Reserved.TryGetValue(s, out var t);
            return t;
        }
    }
}
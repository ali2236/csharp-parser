using System;
using System.Collections.Generic;

namespace csharpAnalyzer
{
    public class Token
    {
        public TokenType TokenType { get; set; }
        public string Attribute { get; set; }

        public Token(TokenType tokenType, String attribute)
        {
            this.TokenType = tokenType;
            this.Attribute = attribute;
        }

        public override string ToString()
        {
            return $"{TokenType}({Attribute})";
        }

        protected bool Equals(Token other) {
            return TokenType == other.TokenType && Attribute == other.Attribute;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Token) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((int) TokenType * 397) ^ (Attribute != null ? Attribute.GetHashCode() : 0);
            }
        }
    }
}
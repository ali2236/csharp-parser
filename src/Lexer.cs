using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using static System.Char;

namespace csharpAnalyzer {
    public class Lexer {
        private CsharpDictionary Dictionary = new CsharpDictionary();

        public IEnumerable<Token> Tokenize(string source) {
            var reader = new StringReader(source);
            while (reader.Peek()!=-1) {
                var ch = (char) reader.Read();
                if (ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r') continue;
                if (IsDigit(ch)) {
                    StringBuilder stringBuilder = new StringBuilder();
                    do {
                        stringBuilder.Append(ch);
                        if (IsDigit((char) reader.Peek())) ch = (char) reader.Read();
                        else break;
                    } while (true);

                    yield return new Token(TokenType.Number, stringBuilder.ToString());
                }
                else if (IsLetter(ch)) {
                    StringBuilder stringBuilder = new StringBuilder();
                    do {
                        stringBuilder.Append(ch);
                        if (IsLetterOrDigit((char) reader.Peek())) ch = (char) reader.Read();
                        else break;
                    } while (true);

                    var word = stringBuilder.ToString();
                    var token = Dictionary.Identify(word);
                    if (token != null) yield return token;
                    else yield return new Token(TokenType.Id, word);
                }
                else { // symbols
                    var peek = (char) reader.Peek();
                    var s1 = Dictionary.Identify(ch.ToString());
                    var s2 = Dictionary.Identify($"{ch}{peek}");
                    if (s2 != null) {
                        reader.Read();
                        yield return s2;
                    }
                    else if (s1 != null) yield return s1;
                    else throw new Exception($"char({ch},peek:{peek}) was not expected!");
                }
            }
        }
    }
}
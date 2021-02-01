using System;
using System.IO;
using System.Linq;

namespace csharpAnalyzer {
    class Program {
        
        static void Main() => Check(File.ReadAllText("../../../inputs/input2.txt"), true);
        static void Check(String source, bool printTokens) {
            try {
                var tokens = new Lexer().Tokenize(source);
                if (printTokens) tokens.ToList().ForEach(t => Console.WriteLine(t));
                new Parser(tokens.ToList()).parse();
                Console.WriteLine("Accepted!");
                Console.WriteLine("##### Tokens #####");
            }
            catch (Exception e) {
                Console.WriteLine("Not Accepted!");
                Console.WriteLine("###############################");
                Console.WriteLine(e);
                throw;
            }
        }
        
    }
}
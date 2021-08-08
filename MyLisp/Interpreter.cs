using System;
using System.Collections.Generic;

namespace MyLisp
{
    public class Interpreter : IInterpreter
    {
        public void Run(string input)
        {
            var tokenizer = new Tokenizer(input);
            var tokens = tokenizer.Tokenize();

            Print(Evaluate(tokens));
        }

        public void Repl()
        {
            while (true)
            {
                var input = Read();

                var tokenizer = new Tokenizer(input);
                var tokens = tokenizer.Tokenize();

                Print(Evaluate(tokens));

                break;
            }
        }

        public string Read()
        {
            Console.Write(">");
            return Console.ReadLine();
        }

        public string Evaluate(List<Token> tokens)
        {
            foreach (var token in tokens)
            {
                Console.WriteLine($"Token {token}");
            }

            return "[eval]";
        }

        public void Print(string output)
        {
            Console.WriteLine($"Result '{output}'");
        }
    }
}

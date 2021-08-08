using System.Collections.Generic;

namespace MyLisp
{
    public interface IInterpreter
    {
        void Run(string input);

        void Repl();

        string Read();

        string Evaluate(List<Token> tokens);

        void Print(string output);
    }
}

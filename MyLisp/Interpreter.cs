using System;

namespace MyLisp
{
    public class Interpreter : IInterpreter
    {
        public void Run(string input)
        {
            Print(Evaluate(input));
        }

        public void Repl()
        {
            while (true)
            {
                var input = Read();
                Print(Evaluate(input));

                break;
            }
        }

        public string Read()
        {
            Console.Write(">");
            return Console.ReadLine();
        }

        public string Evaluate(string input)
        {
            return "[eval]";
        }

        public void Print(string output)
        {
            Console.WriteLine($"Result '{output}'");
        }
    }
}

namespace MyLisp
{
    public interface IInterpreter
    {
        void Run(string input);

        void Repl();

        string Read();

        string Evaluate(string input);

        void Print(string output);
    }
}

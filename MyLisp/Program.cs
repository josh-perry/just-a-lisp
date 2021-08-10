namespace MyLisp
{
    class Program
    {
        static void Main(string[] args)
        {
            var interpreter = new Interpreter(new Parser(), new Tokenizer());
            interpreter.Repl();
        }
    }
}

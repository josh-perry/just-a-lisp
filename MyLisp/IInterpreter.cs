using System.Collections.Generic;

namespace MyLisp
{
    public interface IInterpreter
    {
        ExpressionResult Run(string input);

        void Repl();

        string Read();

        ExpressionResult Evaluate(SExp expression);

        void Print(ExpressionResult result);
    }
}

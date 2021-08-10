using System.Collections.Generic;
using MyLisp.ExpressionResults;
using MyLisp.Expressions;

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

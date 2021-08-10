using System.Collections.Generic;
using MyLisp.Expressions;

namespace MyLisp
{
    public interface IParser
    {
        SExp Parse(Queue<Token> tokens);
    }
}

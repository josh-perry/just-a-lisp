using System.Collections.Generic;

namespace MyLisp.Expressions
{
    public class SExpList : SExp
    {
        public readonly List<SExp> Expressions = new();
    }
}

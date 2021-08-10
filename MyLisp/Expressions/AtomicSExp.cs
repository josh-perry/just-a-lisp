namespace MyLisp.Expressions
{
    public class AtomicSExp : SExp
    {
        public readonly Token Token;

        public AtomicSExp(Token token)
        {
            Token = token;
        }
    }
}

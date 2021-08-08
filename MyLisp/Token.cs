namespace MyLisp
{
    public abstract class Token
    {

    }

    public class Token<T> : Token
    {
        public TokenType TokenType;

        public T Value;
    }
}

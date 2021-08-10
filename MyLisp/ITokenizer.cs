using System.Collections.Generic;

namespace MyLisp
{
    public interface ITokenizer
    {
        List<Token> Tokenize(string input);
    }
}

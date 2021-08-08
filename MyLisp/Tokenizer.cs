using System;
using System.Collections.Generic;

namespace MyLisp
{
    public class Tokenizer : ITokenizer
    {
        private Dictionary<string, TokenType> SingleCharacterTokens { get; set; }

        private int CurrentIndex { get; set; }

        private int PeekIndex { get; set; }

        private string Input { get; set; }

        public Tokenizer(string input)
        {
            SingleCharacterTokens = new Dictionary<string, TokenType>
            {
                { "(", TokenType.LeftParen },
                { ")", TokenType.RightParen },
                { "+", TokenType.Plus }
            };

            Input = input;
        }

        private char ReadNext()
        {
            if (CurrentIndex >= Input.Length)
            {
                throw new IndexOutOfRangeException("Reading past the end of input string!");
            }

            return Input[CurrentIndex++];
        }

        private char PeekNext()
        {
            return Input[PeekIndex++];
        }

        private Token<int> TokenizeNumber()
        {
            // Add the character we just read
            var chars = new List<char>
            {
                Input[CurrentIndex - 1]
            };

            while (true)
            {
                var next = PeekNext();

                if (!char.IsNumber(next))
                {
                    break;
                }

                chars.Add(ReadNext());
            }

            return new Token<int>
            {
                TokenType = TokenType.Number,
                Value = int.Parse(new string(chars.ToArray()))
            };
        }

        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();

            CurrentIndex = 0;

            while (CurrentIndex <= Input.Length - 1)
            {
                var c = ReadNext();
                PeekIndex = CurrentIndex;

                if (SingleCharacterTokens.ContainsKey(c.ToString()))
                {
                    tokens.Add(new Token<object>
                    {
                        TokenType = SingleCharacterTokens[c.ToString()]
                    });

                    continue;
                }

                if (char.IsNumber(c))
                {
                    tokens.Add(TokenizeNumber());
                    continue;
                }
            }

            return tokens;
        }
    }
}

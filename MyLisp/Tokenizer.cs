using System;
using System.Collections.Generic;

namespace MyLisp
{
    public class Tokenizer : ITokenizer
    {
        private Dictionary<string, TokenType> SingleCharacterTokens { get; set; }

        public Tokenizer()
        {
            SingleCharacterTokens = new Dictionary<string, TokenType>
            {
                { "(", TokenType.LeftParen },
                { ")", TokenType.RightParen },
                { "+", TokenType.Plus }
            };
        }

        private char ReadNext(TokenizeState state)
        {
            if (state.Index >= state.Input.Length)
            {
                throw new IndexOutOfRangeException("Reading past the end of input string!");
            }

            return state.Input[state.Index++];
        }

        private char PeekNext(TokenizeState state)
        {
            return state.Input[state.PeekIndex++];
        }

        private Token TokenizeNumber(TokenizeState state)
        {
            // Add the character we just read
            var chars = new List<char>
            {
                state.Input[state.Index - 1]
            };

            while (true)
            {
                var next = PeekNext(state);

                if (!char.IsNumber(next))
                {
                    break;
                }

                chars.Add(ReadNext(state));
            }

            return new Token
            {
                TokenType = TokenType.Number,
                Value = new string(chars.ToArray())
            };
        }

        public List<Token> Tokenize(string input)
        {
            var tokens = new List<Token>();
            var state = new TokenizeState(input);

            while (state.Index <= state.Input.Length - 1)
            {
                var c = ReadNext(state);
                state.PeekIndex = state.Index;

                if (SingleCharacterTokens.ContainsKey(c.ToString()))
                {
                    tokens.Add(new Token
                    {
                        TokenType = SingleCharacterTokens[c.ToString()]
                    });

                    continue;
                }

                if (char.IsNumber(c) || c == '-' || c == '+')
                {
                    tokens.Add(TokenizeNumber(state));
                    continue;
                }
            }

            return tokens;
        }

        private class TokenizeState
        {
            public string Input;

            public int Index = 0;

            public int PeekIndex = 0;

            public TokenizeState(string input)
            {
                Input = input;
            }
        }
    }
}

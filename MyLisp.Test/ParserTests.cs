using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace MyLisp.Test
{
    public class ParserTests
    {
        [Fact]
        public void Parser_Test()
        {
            // Arrange
            var tokens = new List<Token>
            {
                new()
                {
                    TokenType = TokenType.LeftParen
                },

                new()
                {
                    TokenType = TokenType.Plus
                },

                new()
                {
                    TokenType = TokenType.Number,
                    Value = "2"
                },

                new()
                {
                    TokenType = TokenType.Number,
                    Value = "4"
                },

                new()
                {
                    TokenType = TokenType.RightParen,
                }
            };

            var tokenStack = new Queue<Token>(tokens);
            var parser = new Parser();

            // Act
            var sexp = parser.Parse(tokenStack);

            // Assert
        }
    }
}
using System.Collections.Generic;
using MyLisp.Expressions;
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

        [Fact]
        public void Parser_Nested()
        {
            // Arrange
            var tokens = new Tokenizer().Tokenize("(+ 2 (+ 1 1))");
            var parser = new Parser();

            // Act
            var sexp = parser.Parse(new Queue<Token>(tokens));

            // Assert
            Assert.True(sexp.GetType() == typeof(SExpList));

            var sexpList = (SExpList) sexp;
            Assert.True(sexpList.Expressions.Count == 3);
            Assert.True(sexpList.Expressions[0].GetType() == typeof(AtomicSExp));
            Assert.True(((AtomicSExp) sexpList.Expressions[0]).Token.TokenType == TokenType.Plus);

            Assert.True(sexpList.Expressions[1].GetType() == typeof(AtomicSExp));
            Assert.True(((AtomicSExp) sexpList.Expressions[1]).Token.TokenType == TokenType.Number);
            Assert.True(((AtomicSExp) sexpList.Expressions[1]).Token.Value == "2");

            Assert.True(sexpList.Expressions[2].GetType() == typeof(SExpList));
            Assert.True(((SExpList) sexpList.Expressions[2]).Expressions.Count == 3);
        }

        [Fact]
        public void Parser_NestedFirst()
        {
            // Arrange
            var tokens = new Tokenizer().Tokenize("(+ (+ 1 1) 2)");
            var parser = new Parser();

            // Act
            var sexp = parser.Parse(new Queue<Token>(tokens));

            // Assert
            Assert.True(sexp.GetType() == typeof(SExpList));

            var sexpList = (SExpList) sexp;
            Assert.True(sexpList.Expressions.Count == 3);
            Assert.True(sexpList.Expressions[0].GetType() == typeof(AtomicSExp));
            Assert.True(((AtomicSExp) sexpList.Expressions[0]).Token.TokenType == TokenType.Plus);

            Assert.True(sexpList.Expressions[1].GetType() == typeof(SExpList));
            Assert.True(((SExpList) sexpList.Expressions[1]).Expressions.Count == 3);

            Assert.True(sexpList.Expressions[2].GetType() == typeof(AtomicSExp));
            Assert.True(((AtomicSExp) sexpList.Expressions[2]).Token.TokenType == TokenType.Number);
            Assert.True(((AtomicSExp) sexpList.Expressions[2]).Token.Value == "2");
        }
    }
}

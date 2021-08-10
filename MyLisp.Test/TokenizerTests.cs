using Xunit;

namespace MyLisp.Test
{
    public class TokenizerTests
    {
        [Fact]
        public void Tokenizer_Plus2_2_5Tokens()
        {
            // Arrange
            var tokenizer = new Tokenizer("(+ 2 2)");

            // Act
            var tokens = tokenizer.Tokenize();

            // Assert
            Assert.True(tokens.Count == 5);
            Assert.True(tokens[0].TokenType == TokenType.LeftParen);
            Assert.True(tokens[1].TokenType == TokenType.Plus);
            Assert.True(tokens[2].TokenType == TokenType.Number);
            Assert.True(tokens[2].Value == "2");
            Assert.True(tokens[3].Value == "2");
            Assert.True(tokens[3].TokenType == TokenType.Number);
            Assert.True(tokens[4].TokenType == TokenType.RightParen);
        }

        [Fact]
        public void Tokenizer_Plus2_99_5Tokens()
        {
            // Arrange
            var tokenizer = new Tokenizer("(+ 2 99)");

            // Act
            var tokens = tokenizer.Tokenize();

            // Assert
            Assert.True(tokens.Count == 5);
            Assert.True(tokens[0].TokenType == TokenType.LeftParen);
            Assert.True(tokens[1].TokenType == TokenType.Plus);
            Assert.True(tokens[2].TokenType == TokenType.Number);
            Assert.True(tokens[2].Value == "2");
            Assert.True(tokens[3].Value == "99");
            Assert.True(tokens[3].TokenType == TokenType.Number);
            Assert.True(tokens[4].TokenType == TokenType.RightParen);
        }

        [Fact]
        public void Tokenizer_Plus2_Minus3_5Tokens()
        {
            // Arrange
            var tokenizer = new Tokenizer("(+ 2 -3)");

            // Act
            var tokens = tokenizer.Tokenize();

            // Assert
            Assert.True(tokens.Count == 5);
            Assert.True(tokens[0].TokenType == TokenType.LeftParen);
            Assert.True(tokens[1].TokenType == TokenType.Plus);
            Assert.True(tokens[2].TokenType == TokenType.Number);
            Assert.True(tokens[2].Value == "2");
            Assert.True(tokens[3].Value == "-3");
            Assert.True(tokens[3].TokenType == TokenType.Number);
            Assert.True(tokens[4].TokenType == TokenType.RightParen);
        }
    }
}

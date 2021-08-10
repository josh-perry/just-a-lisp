using Xunit;

namespace MyLisp.Test
{
    public class TokenizerTests
    {
        [Fact]
        public void Tokenizer_Plus2_2_5Tokens()
        {
            // Arrange
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Tokenize("(+ 2 2)");

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
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Tokenize("(+ 2 99)");

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
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Tokenize("(+ 2 -3)");

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

        [Fact]
        public void Tokenizer_MultipleNested()
        {
            // Arrange
            var tokenizer = new Tokenizer();

            // Act
            var tokens = tokenizer.Tokenize("(+(+ 1 1) (+ 2 2))");

            // Assert
            Assert.True(tokens.Count == 13);
            Assert.True(tokens[0].TokenType == TokenType.LeftParen);

            Assert.True(tokens[1].TokenType == TokenType.Plus);
            Assert.True(tokens[2].TokenType == TokenType.LeftParen);
            Assert.True(tokens[3].TokenType == TokenType.Plus);
            Assert.True(tokens[4].TokenType == TokenType.Number);
            Assert.True(tokens[4].Value == "1");
            Assert.True(tokens[5].TokenType == TokenType.Number);
            Assert.True(tokens[5].Value == "1");
            Assert.True(tokens[6].TokenType == TokenType.RightParen);

            Assert.True(tokens[7].TokenType == TokenType.LeftParen);
            Assert.True(tokens[8].TokenType == TokenType.Plus);
            Assert.True(tokens[9].TokenType == TokenType.Number);
            Assert.True(tokens[9].Value == "2");
            Assert.True(tokens[10].TokenType == TokenType.Number);
            Assert.True(tokens[10].Value == "2");

            Assert.True(tokens[11].TokenType == TokenType.RightParen);
            Assert.True(tokens[12].TokenType == TokenType.RightParen);
        }
    }
}

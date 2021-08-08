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
        }
    }
}

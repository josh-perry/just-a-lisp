using MyLisp.ExpressionResults;
using Xunit;

namespace MyLisp.Test
{
    public class MultiplyTests
    {
        [Theory]
        [InlineData("(* 8 2)", 16)]
        [InlineData("(* 100 -30)", -3000)]
        [InlineData("(* -30 100)", -3000)]
        [InlineData("(* -5 -10)", 50)]
        public void Multiply_SingleExpression(string code, int expected)
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run(code);

            // Assert
            Assert.Equal(expected, ((ExpressionNumberResult) result).Result);
        }

        [Theory]
        [InlineData("(* 2 (* 80 50))", 8000)]
        public void Multiply_NestedExpression(string code, int expected)
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run(code);

            // Assert
            Assert.Equal(expected, ((ExpressionNumberResult) result).Result);
        }
    }
}

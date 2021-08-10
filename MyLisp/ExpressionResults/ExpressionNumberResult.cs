namespace MyLisp.ExpressionResults
{
    public class ExpressionNumberResult : ExpressionResult
    {
        public readonly int Result;

        public ExpressionNumberResult(int result)
        {
            Result = result;
        }

        public override string ToString()
        {
            return $"NumberResult [{Result}]";
        }
    }
}

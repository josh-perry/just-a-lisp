using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLisp
{
    public abstract class ExpressionResult
    {
    }

    public class ExpressionNumberResult : ExpressionResult
    {
        public int Result;

        public ExpressionNumberResult(int result)
        {
            Result = result;
        }

        public override string ToString()
        {
            return $"NumberResult [{Result}]";
        }
    }

    public class Interpreter : IInterpreter
    {
        private Parser Parser;

        public Interpreter()
        {
            Parser = new Parser();
        }

        public ExpressionResult Run(string input)
        {
            var tokenizer = new Tokenizer(input);
            var tokens = tokenizer.Tokenize();

            var result = Evaluate(Parser.Parse(new Queue<Token>(tokens)));
            Print(result);

            return result;
        }

        public void Repl()
        {
            while (true)
            {
                var input = Read();

                if (input == "q")
                    break;

                var tokenizer = new Tokenizer(input);
                var tokens = tokenizer.Tokenize();

                Print(Evaluate(Parser.Parse(new Queue<Token>(tokens))));
            }
        }

        public string Read()
        {
            Console.Write(">");
            return Console.ReadLine();
        }

        public ExpressionResult Evaluate(SExp expression)
        {
            if (expression.GetType() == typeof(SExpList))
            {
                return EvaluateExpressionList(expression as SExpList);
            }

            return EvaluateAtomicExpression(expression as AtomicSExp);
        }

        public ExpressionResult EvaluateExpressionList(SExpList expressionList)
        {
            var firstExpression = expressionList.Expressions.First();
            if (firstExpression.GetType() == typeof(AtomicSExp))
            {
                var expression = firstExpression as AtomicSExp;

                if (expression.Token.TokenType == TokenType.Plus)
                {
                    return EvaluatePlus(expressionList);
                }
            }

            return new ExpressionNumberResult(0);
        }

        public ExpressionResult EvaluatePlus(SExpList expressionList)
        {
            var total = 0;

            foreach (var expression in expressionList.Expressions.Skip(1))
            {
                var result = Evaluate(expression);

                if (result.GetType() != typeof(ExpressionNumberResult))
                {
                    throw new Exception("Not a numeric result!");
                }

                total += ((ExpressionNumberResult) result).Result;
            }

            return new ExpressionNumberResult(total);
        }

        public ExpressionResult EvaluateAtomicExpression(AtomicSExp expression)
        {
            switch (expression.Token.TokenType)
            {
                case TokenType.Number:
                    if (!int.TryParse(expression.Token.Value, out var o))
                        throw new Exception($"Failed to parse '{expression.Token.Value}' as a number!");

                    return new ExpressionNumberResult(o);
            }

            throw new Exception("The atomic expression didn't match any of the supported token types!");
        }

        public void Print(ExpressionResult result)
        {
            Console.WriteLine(result.ToString());
        }
    }
}

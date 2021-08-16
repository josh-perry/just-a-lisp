using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MyLisp.ExpressionResults;
using MyLisp.Expressions;

namespace MyLisp
{
    public class Interpreter : IInterpreter
    {
        private readonly IParser Parser;

        private readonly ITokenizer Tokenizer;

        public Interpreter(IParser parser, ITokenizer tokenizer)
        {
            Parser = parser;
            Tokenizer = tokenizer;
        }

        public ExpressionResult Run(string input)
        {
            var tokens = Tokenizer.Tokenize(input);

            var result = Evaluate(Parser.Parse(new Queue<Token>(tokens)));
            Print(result);

            return result;
        }

        public void Repl()
        {
            Console.WriteLine("MyLisp");

            while (true)
            {
                var input = Read();

                if (input == "q")
                    break;

                var tokens = Tokenizer.Tokenize(input);

                Print(Evaluate(Parser.Parse(new Queue<Token>(tokens))));
            }

            Console.WriteLine("Goodbye!");
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

                Debug.Assert(expression != null, nameof(expression) + " != null");

                switch (expression.Token.TokenType)
                {
                    case TokenType.Plus:
                        return EvaluateMath(expressionList, TokenType.Plus);
                    case TokenType.Minus:
                        return EvaluateMath(expressionList, TokenType.Minus);
                    case TokenType.Multiply:
                        return EvaluateMath(expressionList, TokenType.Multiply);
                    case TokenType.Divide:
                        return EvaluateMath(expressionList, TokenType.Divide);
                    default:
                        throw new ArgumentException("Expected a math operator!");
                }
            }

            throw new Exception("First expression in expression list isn't atomic");
        }

        public ExpressionResult EvaluateMath(SExpList expressionList, TokenType tokenType)
        {
            var total = 0;
            var first = true;

            foreach (var expression in expressionList.Expressions.Skip(1))
            {
                var result = Evaluate(expression);

                if (result.GetType() != typeof(ExpressionNumberResult))
                {
                    throw new Exception("Not a numeric result!");
                }

                if (first)
                {
                    first = false;
                    total = ((ExpressionNumberResult) result).Result;
                    continue;
                }

                switch (tokenType)
                {
                    case TokenType.Divide:
                        total /= ((ExpressionNumberResult) result).Result;
                        break;
                    case TokenType.Minus:
                        total -= ((ExpressionNumberResult) result).Result;
                        break;
                    case TokenType.Multiply:
                        total *= ((ExpressionNumberResult) result).Result;
                        break;
                    case TokenType.Plus:
                        total += ((ExpressionNumberResult) result).Result;
                        break;
                }
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

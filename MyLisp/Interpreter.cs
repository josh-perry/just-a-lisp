using System;
using System.Collections.Generic;
using System.Linq;
using MyLisp.ExpressionResults;
using MyLisp.Expressions;

namespace MyLisp
{
    public class Interpreter : IInterpreter
    {
        private Parser Parser;

        public Interpreter()
        {
            Parser = new Parser();
        }

        public ExpressionResult Run(string input)
        {
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(input);

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

                var tokenizer = new Tokenizer();
                var tokens = tokenizer.Tokenize(input);

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

                if (expression.Token.TokenType == TokenType.Plus)
                {
                    return EvaluatePlus(expressionList);
                }
            }

            throw new Exception("First expression in expression list isn't atomic");
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

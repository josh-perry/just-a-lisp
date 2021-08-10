using System;
using System.Collections.Generic;
using MyLisp.Expressions;

namespace MyLisp
{
    public class Parser : IParser
    {
        public SExp Parse(Queue<Token> tokens)
        {
            var token = tokens.Dequeue();

            if (token.TokenType == TokenType.RightParen)
                throw new Exception("Unexpected ')'.");

            if (token.TokenType == TokenType.LeftParen)
            {
                var newTree = new SExpList();

                while (tokens.Peek().TokenType != TokenType.RightParen)
                {
                    newTree.Expressions.Add(Parse(tokens));
                }

                return newTree;
            }

            return new AtomicSExp(token);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLisp
{
    public abstract class SExp
    {
    }

    public class AtomicSExp : SExp
    {
        public readonly Token Token;

        public AtomicSExp(Token token)
        {
            Token = token;
        }
    }

    public class SExpList : SExp
    {
        public readonly List<SExp> Expressions = new();
    }

    public class Parser
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

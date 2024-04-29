using System;
using System.Collections.Generic;

namespace A1.Expressions
{
    internal class NegationExpression : Expression
    {
        Expression child;
        public static Expression Parse(List<Token> tokens)
        {
            if (tokens.Count == 0)
                throw new Exception("no more token for negat :(");

            NegationExpression expression = new NegationExpression();
            if (tokens[0].Type == TokenType.varibale)
            {
                expression.child = Variable.Parse(tokens);
            }
            else if (tokens[0].Type == TokenType.negate)
            {
                tokens.RemoveAt(0);
                expression.child = NegationExpression.Parse(tokens);
            }
            else if (tokens[0].Type == TokenType.bracket)
            {
                tokens.RemoveAt(0);
                expression.child = NegationExpression.Parse(tokens);
            } else
            {
                throw new Exception("i lost in negat :(");
            }

            return expression;
        }

        public override bool Evaluate(Dictionary<string, bool> context)
        {
            return !child.Evaluate(context);
        }
    }
}

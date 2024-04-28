using System;
using System.Collections.Generic;

namespace CalculatorWPF.Expressions
{
    internal class StrongestOperator : Operator
    {
        public override double Evaluate(Dictionary<string, double> variables)
        {
            double leftVal = Left.Evaluate(variables);
            double rightVal = Right.Evaluate(variables);

            return Math.Pow(leftVal, rightVal);
        }

        public override Expression Parse(List<Token> tokens)
        {
            Expression left = new Bracket().Parse(tokens);
            while (tokens.Count > 0 && tokens[0].Type == TokenType.StrongestOperator)
            {
                Expression expression = new StrongestOperator();

                tokens.RemoveAt(0);
                ((StrongestOperator)expression).Left = left;
                ((StrongestOperator)expression).Right = new Bracket().Parse(tokens);

                left = expression;
            }

            return left;
        }
    }
}

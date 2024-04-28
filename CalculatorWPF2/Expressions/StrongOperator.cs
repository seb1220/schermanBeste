using System;
using System.Collections.Generic;

namespace CalculatorWPF.Expressions
{
    internal class StrongOperator : Operator
    {
        public enum Type { multiply, divide }

        public Type Operator { get; set; }

        public StrongOperator()
        {
        }

        public override Expression Parse(List<Token> tokens)
        {
            Expression left = new StrongestOperator().Parse(tokens);
            while (tokens.Count > 0 && tokens[0].Type == TokenType.StrongOperator)
            {
                Expression expression = new StrongOperator();
                switch (tokens[0].Value)
                {
                    case "*":
                        ((StrongOperator)expression).Operator = Type.multiply;
                        break;
                    case "/":
                        ((StrongOperator)expression).Operator = Type.divide;
                        break;
                    default:
                        throw new Exception("Invalid operator");
                }
                tokens.RemoveAt(0);
                ((StrongOperator)expression).Left = left;
                ((StrongOperator)expression).Right = new StrongestOperator().Parse(tokens);

                left = expression;
            }

            return left;
        }

        public override double Evaluate(Dictionary<string, double> variables)
        {
            double leftVal = Left.Evaluate(variables);
            double rightVal = Right.Evaluate(variables);

            if (Operator == Type.multiply)
            {
                return leftVal * rightVal;
            }
            else
            {
                return leftVal / rightVal;
            }
        }
    }
}

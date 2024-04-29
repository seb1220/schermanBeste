using System;
using System.Collections.Generic;

namespace CalculatorWPF.Expressions
{
    internal class WeakOperator : Operator
    {
        public enum Type { plus, minus }

        public Type Operator { get; set; }

        public WeakOperator()
        {
        }

        public override Expression Parse(List<Token> tokens)
        {
            Expression left = new StrongOperator().Parse(tokens);
            while (tokens.Count > 0 && tokens[0].Type == TokenType.WeakOperator)
            {
                Expression expression = new WeakOperator();
                switch (tokens[0].Value)
                {
                    case "+":
                        ((WeakOperator)expression).Operator = Type.plus;
                        break;
                    case "-":
                        ((WeakOperator)expression).Operator = Type.minus;
                        break;
                    default:
                        throw new Exception("Invalid operator");
                }
                tokens.RemoveAt(0);
                ((WeakOperator)expression).Left = left;
                ((WeakOperator)expression).Right = new StrongOperator().Parse(tokens);

                left = expression;
            }

            return left;
        }

        public override double Evaluate(Dictionary<string, double> variables)
        {
            double leftVal = Left.Evaluate(variables);
            double rightVal = Right.Evaluate(variables);
            if (Operator == Type.plus)
            {
                return leftVal + rightVal;
            }
            else
            {
                return leftVal - rightVal;
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace CalculatorWPF.Expressions
{
    internal class Literal : Expression
    {
        public double Value { get; set; }
        public Expression Pre { get; set; }

        public Literal()
        {
        }

        public override Expression Parse(List<Token> tokens)
        {
            Value = Convert.ToDouble(tokens[0].Value);
            tokens.RemoveAt(0);
            return this;
        }

        public override double Evaluate(Dictionary<string, double> variables)
        {
            return Value;
        }
    }
}

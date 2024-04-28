using System.Collections.Generic;

namespace CalculatorWPF.Expressions
{
    internal abstract class Expression
    {
        public abstract Expression Parse(List<Token> tokens);
        public abstract double Evaluate(Dictionary<string, double> variables);
    }
}

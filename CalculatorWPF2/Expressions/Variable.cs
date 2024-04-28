using System;
using System.Collections.Generic;

namespace CalculatorWPF.Expressions
{
    internal class Variable : Expression
    {
        public string Name { get; set; }

        public override Expression Parse(List<Token> tokens)
        {
            Console.WriteLine("new variable");
            Name = tokens[0].Value;
            tokens.RemoveAt(0);
            return this;
        }

        public override double Evaluate(Dictionary<string, double> variables)
        {
            Console.WriteLine($"Variable: {Name}");
            foreach (var Key in variables.Keys)
            {
                Console.WriteLine($"Key: {Key}");
            }
            return variables[Name];
        }
    }
}

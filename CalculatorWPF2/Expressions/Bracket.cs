using System.Collections.Generic;

namespace CalculatorWPF.Expressions
{
    internal class Bracket : Expression
    {
        Expression Expression { get; set; }
        public override double Evaluate(Dictionary<string, double> variables)
        {
            return Expression.Evaluate(variables);
        }

        public override Expression Parse(List<Token> tokens)
        {
            if (tokens[0].Type == TokenType.Number)
            {
                Expression = new Literal().Parse(tokens);
            }
            else if (tokens[0].Type == TokenType.Variable)
            {
                Expression = new Variable().Parse(tokens);
            }
            else
            {
                tokens.RemoveAt(0);
                Expression = new WeakOperator().Parse(tokens);
                tokens.RemoveAt(0);
            }
            return Expression;
        }
    }
}

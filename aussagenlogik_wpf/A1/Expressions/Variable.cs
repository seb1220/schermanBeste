using System.Collections.Generic;

namespace A1
{
    internal class Variable : Expression
    {
        string identifier;

        public Variable()
        {
        }

        public override bool Evaluate(Dictionary<string, bool> context)
        {
            return context[identifier];
        }

        public static Expression Parse(List<Token> formel)
        {
            Variable variable = new Variable();
            variable.identifier = formel[0].Value;
            formel.RemoveAt(0);
            return variable;
        }
    }

}

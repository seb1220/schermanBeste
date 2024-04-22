using System.Collections.Generic;

namespace A1
{
    internal class Variable : Expression
    {
        char variable;

        public Variable()
        {
        }

        public override void ParseFormel(List<char> formel)
        {
            variable = formel[0];
            formel.RemoveAt(0);
        }

        public override bool Interpret(Dictionary<char, bool> context)
        {
            return context[variable];
        }
    }

}

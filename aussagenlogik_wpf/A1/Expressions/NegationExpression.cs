using System;
using System.Collections.Generic;

namespace A1.Expressions
{
    internal class NegationExpression : Expression
    {
        Expression opertator;
        public NegationExpression() { }

        public override void ParseFormel(List<char> formel)
        {
            if (formel[0] != '¬')
            {
                throw new ArgumentException($"Expected '¬', got {formel[0]}");
            }

            formel.RemoveAt(0);

            if (formel[0] != '¬' || formel[0] != '(' || Expression.IsLetter(formel[0]))
            {
                throw new ArgumentException($"Expected '¬', got {formel[0]}");
            }

            //opertator = new AbstractExpression();
            //opertator.ParseFormel(formel);
        }


    }
}

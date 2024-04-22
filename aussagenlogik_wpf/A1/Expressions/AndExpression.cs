using System.Collections.Generic;

namespace A1.Expressions
{
    internal class AndExpression : Expression
    {
        Expression opertator1;
        Expression opertator2;

        public AndExpression()
        {
        }

        public override void ParseFormel(List<char> formel)
        {

        }

        public override bool Interpret(Dictionary<char, bool> context)
        {
            return opertator1.Interpret(context) && opertator2.Interpret(context);
        }
    }
}

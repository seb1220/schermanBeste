using A1.Expressions;
using System.Collections.Generic;

namespace A1
{
    public abstract class Expression
    {
        public static Expression Parse(List<Token> formel)
        {
            return OrExpression.Parse(formel); // TODO
        }
        public abstract bool Evaluate(Dictionary<string, bool> context);
    }
}

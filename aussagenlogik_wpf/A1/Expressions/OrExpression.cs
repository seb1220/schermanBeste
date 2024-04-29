using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1.Expressions
{
    internal class OrExpression : Operator
    {
        public static Expression Parse(List<Token> tokens)
        {
            Expression left = AndExpression.Parse(tokens);

            while (tokens.Count > 0 && tokens[0].Type == TokenType.or) 
            { 
                Operator expression = new OrExpression();
                tokens.RemoveAt(0);

                expression.Left = left;
                expression.Right = AndExpression.Parse(tokens);

                left = expression;
            }

            return left;
        }
        public override bool Evaluate(Dictionary<string, bool> context)
        {
            return Left.Evaluate(context) || Right.Evaluate(context);
        }
    }
}

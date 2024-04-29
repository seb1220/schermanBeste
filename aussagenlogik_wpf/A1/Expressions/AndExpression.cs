using System.Collections.Generic;

namespace A1.Expressions
{
    internal class AndExpression : Operator
    {

        public AndExpression()
        {
        }

        public static Expression Parse (List<Token> tokens)
        {
            Expression left = NegationExpression.Parse(tokens);

            while (tokens.Count > 0 && tokens[0].Type == TokenType.and) 
            {
                Operator expression = new AndExpression();
                tokens.RemoveAt(0);

                expression.Left = left;
                expression.Right = NegationExpression.Parse(tokens);
                left = expression;
            }

            return left;
        }

        public override bool Evaluate(Dictionary<string, bool> context)
        {
            return Left.Evaluate(context) && Right.Evaluate(context);
        }
    }
}

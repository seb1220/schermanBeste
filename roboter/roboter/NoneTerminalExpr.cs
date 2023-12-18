using AbcRobotCore;
using System.Collections.Generic;
using System.Linq;

namespace roboter
{
    internal class NoneTerminalExpr : Expression
    {
        private List<Expression> expressions;

        public NoneTerminalExpr(List<Expression> expressions, Expression pre) : base(pre)
        {
            this.expressions = expressions;
        }

        public override void Parse(List<Token> tokens)
        {
            int endBracket = 1;
            while (tokens.Count > 0)
            {
                if (endBracket <= 0)
                    break;

                Token current = tokens.First();

                switch (current.value.ToUpper())
                {
                    case "REPEAT":
                        Repeat repeat = new Repeat(this);
                        repeat.Parse(tokens);
                        expressions.Add(repeat);
                        break;
                    case "MOVE":
                        Move move = new Move(this);
                        move.Parse(tokens);
                        expressions.Add(move);
                        break;
                    case "COLLECT":
                        Collect collect = new Collect(this);
                        collect.Parse(tokens);
                        expressions.Add(collect);
                        break;
                    case "UNTIL":
                        Until until = new Until(this);
                        until.Parse(tokens);
                        expressions.Add(until);
                        break;
                    case "IF":
                        If if_expr = new If(this);
                        if_expr.Parse(tokens);
                        expressions.Add(if_expr);
                        break;
                    case "{":
                        tokens.RemoveAt(0);
                        endBracket++;
                        break;
                    case "}":
                        tokens.RemoveAt(0);
                        endBracket--;
                        break;
                }
            }
        }

        public override bool Run(RobotField robot)
        {
            bool success = true;
            foreach (Expression expression in expressions)
                if (!expression.Run(robot))
                    success = false;
            return success;
        }
    }
}

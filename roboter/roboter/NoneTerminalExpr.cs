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
                    /* case "UNTIL":
                         expressions.Add(UntilExpr.Parse(tokens));
                         break;
                     case "IF":
                         expressions.Add(IfExpr.Parse(tokens));
                         break;
                     case "IS-A":
                         expressions.Add(IsAExpr.Parse(tokens));
                         break;
                     case "UP":
                         expressions.Add(UpExpr.Parse(tokens));
                         break;
                     case "DOWN":
                         expressions.Add(DownExpr.Parse(tokens));
                         break;
                     case "LEFT":
                         expressions.Add(LeftExpr.Parse(tokens));
                         break;
                     case "RIGHT":
                         expressions.Add(RightExpr.Parse(tokens));
                         break;
                     case "OBSTACLE":
                         expressions.Add(ObstacleExpr.Parse(tokens));
                         break;*/
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

        public override void Run(RobotField robot)
        {
            foreach (Expression expression in expressions)
                expression.Run(robot);
        }
    }
}

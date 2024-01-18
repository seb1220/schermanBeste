using AbcRobotCore;
using System.Collections.Generic;

namespace PA2_UE_Interpreter.Expr
{
    internal class Move : TerminalExpr
    {
        Direction direction;

        public override void Execute(RobotField robot)
        {
            robot.Move(direction.getRobotDirection());
        }

        public override void Parse(List<Token> tokens)
        {
            tokens.RemoveAt(0);

            if (tokens.Count < 1)
            {
                Interpreter.Errors.Add("Missing parameter for move command");
                return;
            }

            direction = new Direction(tokens[0]);
            tokens.RemoveAt(0);
        }
    }
}

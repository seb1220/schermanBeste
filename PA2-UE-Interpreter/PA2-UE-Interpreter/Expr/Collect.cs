using System.Collections.Generic;

namespace PA2_UE_Interpreter.Expr
{
    internal class Collect : TerminalExpr
    {
        public override void Execute(AbcRobotCore.RobotField robot)
        {
            robot.Collect();
        }

        public override void Parse(List<Token> tokens)
        {
            tokens.RemoveAt(0);
        }
    }
}

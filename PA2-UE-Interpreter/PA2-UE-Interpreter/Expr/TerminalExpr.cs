using System.Collections.Generic;

namespace PA2_UE_Interpreter.Expr
{
    internal abstract class TerminalExpr
    {
        public abstract void Parse(List<Token> tokens);
        public abstract void Execute(AbcRobotCore.RobotField robot);
    }
}

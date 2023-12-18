using AbcRobotCore;
using System.Collections.Generic;

namespace roboter
{
    internal class Program
    {
        NoneTerminalExpr Root;
        public static List<string> Errors = new List<string>();

        public Program(List<Token> tokens)
        {
            Root = new NoneTerminalExpr(new List<Expression>(), null);
            Root.Parse(tokens);
        }

        public void run(RobotField robot)
        {
            Root.Run(robot);
        }
    }
}

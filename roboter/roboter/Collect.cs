using AbcRobotCore;
using System;
using System.Collections.Generic;

namespace roboter
{
    internal class Collect : TerminalExpr
    {
        public Collect(Expression pre) : base(pre)
        {
        }

        public override void Parse(List<Token> tokens)
        {
            Console.WriteLine("Processing Collect...");

            tokens.RemoveAt(0);

            Console.WriteLine("Processed Collect.");
        }

        public override bool Run(RobotField robot)
        {
            robot.Collect();
            return true;
        }
    }
}

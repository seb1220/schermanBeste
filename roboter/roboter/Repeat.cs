using AbcRobotCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace roboter
{
    internal class Repeat : TerminalExpr
    {
        int count;
        Block block;

        public Repeat(Expression pre) : base(pre)
        {
        }

        public override void Parse(List<Token> tokens)
        {
            Console.WriteLine("Processing Repeat...");

            tokens.RemoveAt(0);

            if (tokens.Count < 3)
            {
                Program.Errors.Add("Errors while parsing REPEAT: program ended unexpectedly");
                return;
            }

            if (!int.TryParse(tokens.First().value, out count))
                Program.Errors.Add("Errors while parsing REPEAT: no valid count given");

            tokens.RemoveAt(0);

            tokens.RemoveAt(0);
            block = new Block(new List<Expression>(), this);
            block.Parse(tokens);

            Console.WriteLine("Processed Repeat.");
        }

        public override bool Run(RobotField robot)
        {
            bool success = true;
            for (int i = 0; i < count; ++i)
                if (!block.Run(robot))
                    success = false;
            return success;
        }
    }
}

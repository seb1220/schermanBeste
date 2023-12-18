using AbcRobotCore;
using System;
using System.Collections.Generic;

namespace roboter
{
    internal class If : TerminalExpr
    {
        Condition condition;
        Block block;
        public If(Expression pre) : base(pre)
        {
        }

        public override void Parse(List<Token> tokens)
        {
            Console.WriteLine("Processing If...");

            tokens.RemoveAt(0);

            if (tokens.Count == 0)
            {
                Program.Errors.Add("Errors while parsing MOVE: program ended unexpectedly");
                return;
            }

            condition = new Condition(this);
            condition.Parse(tokens);

            tokens.RemoveAt(0);
            block = new Block(new List<Expression>(), this);
            block.Parse(tokens);

            Console.WriteLine("Processed If.");
        }

        public override bool Run(RobotField robot)
        {
            if (condition.Run(robot))
            {
                Console.WriteLine("Condition is true, executing block...");
                return block.Run(robot);
            }
            Console.WriteLine("Condition is false, skipping block...");
            return true;
        }
    }
}

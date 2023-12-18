using AbcRobotCore;
using System;
using System.Collections.Generic;

namespace roboter
{
    internal class Until : TerminalExpr
    {
        Condition condition;
        Block block;

        public Until(Expression pre) : base(pre)
        {
        }

        public override void Parse(List<Token> tokens)
        {
            Console.WriteLine("Processing Until...");

            tokens.RemoveAt(0);

            if (tokens.Count < 4)
            {
                Program.Errors.Add("Errors while parsing REPEAT: program ended unexpectedly");
                return;
            }

            condition = new Condition(this);
            condition.Parse(tokens);

            tokens.RemoveAt(0);
            block = new Block(new List<Expression>(), this);
            block.Parse(tokens);

            Console.WriteLine("Processed Until.");
        }

        public override bool Run(RobotField robot)
        {
            Console.WriteLine("Executing block Until condition...");
            while (!condition.Run(robot))
            {
                block.Run(robot);
            }
            Console.WriteLine("Exiting Until.");
            return true;
        }
    }
}

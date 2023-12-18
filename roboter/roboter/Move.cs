using AbcRobotCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace roboter
{
    internal class Move : TerminalExpr
    {
        RobotField.Direction dir;

        public Move(Expression pre) : base(pre)
        {
        }

        public override void Parse(List<Token> tokens)
        {
            Console.WriteLine("Processing Move...");

            tokens.RemoveAt(0);

            if (tokens.Count == 0)
            {
                Program.Errors.Add("Errors while parsing MOVE: program ended unexpectedly");
                return;
            }

            Console.WriteLine(tokens.First().value);
            switch (tokens.First().value.ToUpper())
            {
                case "UP":
                    dir = RobotField.Direction.Up;
                    break;
                case "DOWN":
                    dir = RobotField.Direction.Down;
                    break;
                case "LEFT":
                    dir = RobotField.Direction.Left;
                    break;
                case "RIGHT":
                    dir = RobotField.Direction.Right;
                    break;
                default:
                    Program.Errors.Add("Errors while parsing MOVE: no valid Direction given");
                    break;
            }
            tokens.RemoveAt(0);
            Console.WriteLine("Processed Move.");
        }

        public override bool Run(RobotField robot)
        {
            return robot.Move(dir);
        }
    }
}

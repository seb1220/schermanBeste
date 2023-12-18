using AbcRobotCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace roboter
{
    internal class Condition : TerminalExpr
    {
        RobotField.Direction dir;
        RobotField.FieldType type;
        string letter;

        public Condition(Expression pre) : base(pre)
        {
        }

        public override void Parse(List<Token> tokens)
        {
            Console.WriteLine("Processing Condition...");

            if (tokens.Count < 3)
            {
                Program.Errors.Add("Errors while parsing CONDITION: program ended unexpectedly");
                return;
            }

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
                    Program.Errors.Add("Errors while parsing Condition: no valid Direction given");
                    break;
            }
            tokens.RemoveAt(0);

            if (tokens.First().value != "IS-A")
            {
                Program.Errors.Add("Errors while parsing CONDITION: did you mean IS-A?");
                return;
            }
            tokens.RemoveAt(0);

            if (!(tokens.First().type == token_type.fieldType))
            {
                Program.Errors.Add("Errors while parsing CONDITION: no valid FieldType given");
                return;
            }

            switch (tokens.First().value.ToUpper())
            {
                case "OBSTACLE":
                    type = RobotField.FieldType.Wall;
                    break;
                case "A":
                    letter = "A";
                    type = RobotField.FieldType.Letter;
                    break;
                case "B":
                    letter = "B";
                    type = RobotField.FieldType.Letter;
                    break;
                case "C":
                    letter = "C";
                    type = RobotField.FieldType.Letter;
                    break;
                case "D":
                    letter = "D";
                    type = RobotField.FieldType.Letter;
                    break;
                case "E":
                    letter = "E";
                    type = RobotField.FieldType.Letter;
                    break;
                case "F":
                    letter = "F";
                    type = RobotField.FieldType.Letter;
                    break;
                default:
                    Program.Errors.Add("Errors while parsing Condition: no valid FieldType given");
                    break;
            }
            tokens.RemoveAt(0);

            Console.WriteLine("Processed Condition.");
        }

        public override bool Run(RobotField robot)
        {
            if (type == RobotField.FieldType.Letter)
            {
                return robot.IsLetter(letter, dir);
            }
            else if (type == RobotField.FieldType.Wall)
            {
                return robot.IsObstacle(dir);
            }
            else
            {
                Program.Errors.Add("Errors while parsing Condition: no valid FieldType given");
                return false;
            }
        }
    }
}

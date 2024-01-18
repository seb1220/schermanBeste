using System.Collections.Generic;

namespace PA2_UE_Interpreter.Expr
{
    internal class Repeat : TerminalExpr
    {
        private int count;
        private Block block;

        public override void Parse(List<Token> tokens)
        {
            tokens.RemoveAt(0);
            if (tokens.Count < 2)
            {
                Interpreter.Errors.Add("Missing count or block");
                return;
            }

            count = int.Parse(tokens[0].Value);
            tokens.RemoveAt(0);
            block = new Block();
            block.Parse(tokens);
        }

        public override void Execute(AbcRobotCore.RobotField robot)
        {
            for (int i = 0; i < count; i++)
            {
                block.Execute(robot);
            }
        }
    }
}

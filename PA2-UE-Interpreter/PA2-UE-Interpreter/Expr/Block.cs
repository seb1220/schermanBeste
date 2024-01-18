using System.Collections.Generic;
using System.Diagnostics;

namespace PA2_UE_Interpreter.Expr
{
    internal class Block : NoneTerminalExpr
    {

        public int LineNumber { get; private set; }

        public override void Parse(List<Token> tokens)
        {
            Debug.WriteLine("BLOCK 1st...");

            if (tokens.Count > 0)
                LineNumber = tokens[0].Line;
            if (tokens[0].Type == Token.TokenType.OPEN_BRACE)
            {
                tokens.RemoveAt(0);
            }

            while (tokens.Count > 0)
            {
                if (tokens[0].Type == Token.TokenType.REPEAT)
                {
                    expr.Add(new Repeat());
                }
                else if (tokens[0].Type == Token.TokenType.COLLECT)
                {
                    expr.Add(new Collect());
                }
                else if (tokens[0].Type == Token.TokenType.MOVE)
                {
                    expr.Add(new Move());
                }
                else if (tokens[0].Type == Token.TokenType.IF)
                {
                    //expr.Add(new If());
                }
                else if (tokens[0].Type == Token.TokenType.UNTIL)
                {
                    //expr.Add(new Until());
                }
                else if (tokens[0].Type == Token.TokenType.OPEN_BRACE)
                {
                    //expr.Add(new Block());
                }
                else if (tokens[0].Type == Token.TokenType.CLOSE_BRACE)
                {
                    tokens.RemoveAt(0);
                    return;
                }
                else
                {
                    Interpreter.Errors.Add("Unexpected token type: " + tokens[0].Type);
                }

                expr[expr.Count - 1].Parse(tokens);
            }
        }

        public override void Execute(AbcRobotCore.RobotField robot)
        {
            foreach (TerminalExpr e in expr)
            {
                e.Execute(robot);
            }
        }
    }
}

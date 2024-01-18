using System;
using System.Collections.Generic;

namespace PA2_UE_Interpreter.Expr
{
    internal class NoneTerminalExpr : TerminalExpr
    {
        protected List<TerminalExpr> expr;

        public NoneTerminalExpr()
        {
            this.expr = new List<TerminalExpr>();
        }

        public override void Parse(List<Token> tokens)
        {
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
                    //expr.Add(new UntilExpr());
                }
                else if (tokens[0].Type == Token.TokenType.OPEN_BRACE)
                {
                    expr.Add(new Block());
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
            throw new NotImplementedException();
        }
    }
}

using LineDraw;
using PA2_5B_Control;
using System;
using System.Collections.Generic;

namespace PA2_5B
{
    internal class Draw : Expression
    {
        string dir;
        int angle;
        public override void Parse(List<Token> tokenlist)
        {
            tokenlist.RemoveAt(0);

            if (tokenlist[0].Text == "LEFT")
            {
                dir = "LEFT";
            }
            else if (tokenlist[0].Text == "RIGHT")
            {
                dir = "RIGHT";
            }
            else
            {
                Interpreter.Errors.Add(string.Format("Expected LEFT or RIGHT, got {0} at line {1}", tokenlist[0].Text, tokenlist[0].LineNumber));
                return;
            }
            tokenlist.RemoveAt(0);

            if (tokenlist[0].type == Token.Type.Number)
            {
                angle = int.Parse(tokenlist[0].Text);
            }
            else
            {
                Interpreter.Errors.Add(string.Format("Expected number, got {0} at line {1}", tokenlist[0].Text, tokenlist[0].LineNumber));
                return;
            }

            tokenlist.RemoveAt(0);
        }

        public override bool Run(LinePainter painter)
        {
            throw new NotImplementedException();
        }
    }
}

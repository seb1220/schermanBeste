using LineDraw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA2_5B_Control
{
    public abstract class Expression
    {
        public abstract void Parse(List<Token> tokenlist);
        public abstract bool Run(LinePainter painter);
    }
}

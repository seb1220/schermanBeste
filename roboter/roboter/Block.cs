using System.Collections.Generic;

namespace roboter
{
    internal class Block : NoneTerminalExpr
    {
        public Block(List<Expression> expressions, Expression pre) : base(expressions, pre)
        {
        }
    }
}

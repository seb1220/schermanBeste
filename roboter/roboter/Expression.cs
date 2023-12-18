using AbcRobotCore;
using System.Collections.Generic;

namespace roboter
{
    internal abstract class Expression
    {
        protected Expression pre;

        public Expression(Expression pre)
        {
            this.pre = pre;
        }

        public abstract void Parse(List<Token> tokens);

        public abstract void Run(RobotField robot);
    }
}

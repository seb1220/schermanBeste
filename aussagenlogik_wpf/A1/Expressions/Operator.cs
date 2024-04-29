using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1.Expressions
{
    internal abstract class Operator : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }
    }
}

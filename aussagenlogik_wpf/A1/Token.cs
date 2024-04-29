using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    public enum TokenType
    {
        varibale,
        bracket,
        negate,
        and,
        or,
        conditional,
        biconditional
    }
    public class Token
    {
        public TokenType Type;
        public string Value;
    }
}

namespace roboter
{
    internal abstract class TerminalExpr : Expression
    {
        protected TerminalExpr(Expression pre) : base(pre)
        {
        }
    }
}
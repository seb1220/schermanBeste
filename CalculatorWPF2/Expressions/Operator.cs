namespace CalculatorWPF.Expressions
{
    internal abstract class Operator : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }
    }
}

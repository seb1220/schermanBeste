namespace CalculatorWPF
{
    internal class Token
    {
        public string Value { get; set; }
        public TokenType Type { get; set; }
    }

    public enum TokenType { Variable, Number, WeakOperator, StrongOperator, StrongestOperator, Bracket, Error };
}

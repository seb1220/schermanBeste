namespace PA2_5B_Control
{
    public class Token
    {
        public enum Type { Keyword, Number, Error }
        public string Text { get; set; } = string.Empty;
        public Type type { get; set; } = Type.Error;

        public int LineNumber { get; set; } = 0;

        public Token(Type type, int LineNumber, string text)
        {
            this.type = type;
            this.Text = text;
            this.LineNumber = LineNumber;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1}) at line {2}", Text, type, LineNumber);
        }
    }
}
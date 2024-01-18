namespace PA2_UE_Interpreter
{
    internal class Token
    {
        public enum TokenType
        {
            REPEAT,
            MOVE,
            COLLECT,
            UNTIL,
            IF,
            IS_A,
            DIRECTION,
            OBSTACLE,
            NUMBER,
            OPEN_BRACE,
            CLOSE_BRACE,
            ITEM,
        }

        public TokenType Type { get; private set; }
        public string Value { get; private set; }
        public int Line { get; private set; }

        public Token(TokenType type, int line, string value)
        {
            Type = type;
            Value = value;
            Line = line;
        }

        public override string ToString()
        {
            return string.Format("Token({0}, {1} at line {2})", Type, Value, Line);
        }
    }
}

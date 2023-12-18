namespace roboter
{
    enum token_type
    {
        keyword,
        number,
        bracket,
        newline,
        fieldType
    }

    internal class Token
    {
        public token_type type;
        public string value;

        public Token(string value)
        {
            switch (value.ToUpper())
            {
                case "REPEAT":
                case "MOVE":
                case "UNTIL":
                case "IF":
                case "IS-A":
                case "COLLECT":
                case "UP":
                case "DOWN":
                case "LEFT":
                case "RIGHT":
                    type = token_type.keyword;
                    break;
                case "A":
                case "B":
                case "C":
                case "D":
                case "E":
                case "F":
                case "OBSTACLE":
                    type = token_type.fieldType;
                    break;
                case "{":
                case "}":
                    type = token_type.bracket;
                    break;
                default:
                    type = token_type.number;
                    break;
            }
            this.value = value;
        }
    }
}

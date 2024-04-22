using System.Collections.Generic;

namespace A1
{
    public class Expression
    {
        public void ParseFormel(List<char> formel)
        {
            while (formel.Count > 0)
            {
                if (formel[0] == '¬')
                {
                    formel.RemoveAt(0);
                    NegationExpression negation = new NegationExpression();
                    negation.ParseFormel(formel);
                }
                else if (formel[0] == '(')
                {
                    formel.RemoveAt(0);
                    AndExpression and = new AndExpression();
                    and.ParseFormel(formel);
                }
                else if (IsLetter(formel[0]))
                {
                    Variable variable = new Variable();
                    variable.ParseFormel(formel);
                }
                else
                {
                    throw new System.ArgumentException($"Expected '¬', '(', or a letter, got {formel[0]}");
                }
            }
        }

        //Variablenliste wird beim Parsen befüllt
        public static List<char> variables = new List<char>();

        public bool Interpret(Dictionary<char, bool> context)
        {
            return false;
        }

        public static bool IsLetter(char c)
        {
            return c >= 'a' && c <= 'z';
        }
    }
}

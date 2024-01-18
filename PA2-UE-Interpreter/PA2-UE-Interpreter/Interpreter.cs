using PA2_UE_Interpreter.Expr;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace PA2_UE_Interpreter
{
    internal class Interpreter
    {
        AbcRobotCore.RobotField robot;
        List<Token> tokens;
        public static List<string> Errors = new List<string>();
        Block root;

        public Interpreter(AbcRobotCore.RobotField robot)
        {
            this.robot = robot;
            tokens = new List<Token>();
            Errors = new List<string>();
        }

        public void Tokenize(string code)
        {
            var pattern = new Regex(@"(REPEAT|MOVE|COLLECT|UNTIL|IF|IS-A|UP|DOWN|LEFT|RIGHT|OBSTACLE|\d+|{|}|\S+)(\s+|$)", RegexOptions.Compiled);
            MatchCollection matches = pattern.Matches(code);

            int currentLine = 1;
            foreach (Match match in matches)
            {
                if (match.Groups[1].Value == "REPEAT")
                {
                    tokens.Add(new Token(Token.TokenType.REPEAT, currentLine, match.Groups[1].Value));
                }
                else if (match.Groups[1].Value == "MOVE")
                {
                    tokens.Add(new Token(Token.TokenType.MOVE, currentLine, match.Groups[1].Value));
                }
                else if (match.Groups[1].Value == "COLLECT")
                {
                    tokens.Add(new Token(Token.TokenType.COLLECT, currentLine, match.Groups[1].Value));
                }
                else if (match.Groups[1].Value == "UNTIL")
                {
                    tokens.Add(new Token(Token.TokenType.UNTIL, currentLine, match.Groups[1].Value));
                }
                else if (match.Groups[1].Value == "IF")
                {
                    tokens.Add(new Token(Token.TokenType.IF, currentLine, match.Groups[1].Value));
                }
                else if (match.Groups[1].Value == "IS-A")
                {
                    tokens.Add(new Token(Token.TokenType.IS_A, currentLine, match.Groups[1].Value));
                }
                else if (new[] { "UP", "DOWN", "LEFT", "RIGHT" }.Contains(match.Groups[1].Value))
                {
                    tokens.Add(new Token(Token.TokenType.DIRECTION, currentLine, match.Groups[1].Value));
                }
                else if (match.Groups[1].Value == "OBSTACLE")
                {
                    tokens.Add(new Token(Token.TokenType.OBSTACLE, currentLine, match.Groups[1].Value));
                }
                else if (match.Groups[1].Value == "{")
                {
                    tokens.Add(new Token(Token.TokenType.OPEN_BRACE, currentLine, match.Groups[1].Value));
                }
                else if (match.Groups[1].Value == "}")
                {
                    tokens.Add(new Token(Token.TokenType.CLOSE_BRACE, currentLine, match.Groups[1].Value));
                }
                else if (int.TryParse(match.Groups[1].Value, out int _))
                {
                    tokens.Add(new Token(Token.TokenType.NUMBER, currentLine, match.Groups[1].Value));
                }
                else if (match.Groups[1].Value.Length == 1 && Regex.IsMatch(match.Groups[1].Value, @"^[A-Z]$"))
                {
                    tokens.Add(new Token(Token.TokenType.ITEM, currentLine, match.Groups[1].Value));
                }
                else
                {
                    Errors.Add(string.Format("Unknown token '{0}' at line {1}", match.Groups[1].Value, currentLine));
                }

                if (match.Groups[2].Value.Contains('\n'))
                {
                    currentLine++;
                }
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                Debug.WriteLine(tokens[i]);
            }
        }

        public void Parse()
        {
            if (Errors.Count > 0)
            {
                return;
            }

            // Parse here
            root = new Block();
            root.Parse(tokens);

        }

        public void Execute()
        {
            if (Errors.Count > 0)
            {
                return;
            }

            Debug.WriteLine("Running...");
            root.Execute(robot);
        }
    }
}

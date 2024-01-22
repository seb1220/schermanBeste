using LineDraw;
using PA2_5B_Control;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PA2_5B
{
    internal class Interpreter
    {
        List<Token> tokens;
        List<Expression> expr;
        public static List<string> Errors = new List<string>();

        public Interpreter(List<Token> tokens)
        {
            this.tokens = tokens;
            this.expr = new List<Expression>();
        }

        public void Tokenize(string code)
        {
            var pattern = new Regex(@"(TURN|DRAW|LEFT|RIGHT|\d+|\S+)(\s+|$)", RegexOptions.Compiled);
            MatchCollection matches = pattern.Matches(code);

            int currentLine = 1;
            foreach (Match match in matches)
            {
                if (match.Groups[1].Value == "TURN")
                {
                    tokens.Add(new Token(Token.Type.Keyword, currentLine, match.Groups[1].Value));
                }
                else if (match.Groups[1].Value == "DRAW")
                {
                    tokens.Add(new Token(Token.Type.Keyword, currentLine, match.Groups[1].Value));
                }
                else if (match.Groups[1].Value == "LEFT")
                {
                    tokens.Add(new Token(Token.Type.Keyword, currentLine, match.Groups[1].Value));
                }
                else if (match.Groups[1].Value == "RIGHT")
                {
                    tokens.Add(new Token(Token.Type.Keyword, currentLine, match.Groups[1].Value));
                }
                else if (int.TryParse(match.Groups[1].Value, out int _))
                {
                    tokens.Add(new Token(Token.Type.Number, currentLine, match.Groups[1].Value));
                }
                else
                {
                    tokens.Add(new Token(Token.Type.Error, currentLine, match.Groups[1].Value));
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
            while (tokens.Count > 0)
            {
                if (tokens[0].type == Token.Type.Keyword)
                {
                    if (tokens[0].Text == "TURN")
                    {
                        expr.Add(new Turn());
                    }
                    else if (tokens[0].Text == "DRAW")
                    {
                        expr.Add(new Draw());
                    }
                    else
                    {
                        Errors.Add(string.Format("Unknown keyword '{0}' at line {1}", tokens[0].Text, tokens[0].LineNumber));
                        return;
                    }
                }
                else if (tokens[0].type == Token.Type.Number)
                {
                    Errors.Add(string.Format("Unexpected number '{0}' at line {1}", tokens[0].Text, tokens[0].LineNumber));
                    return;
                }
                else if (tokens[0].type == Token.Type.Error)
                {
                    Errors.Add(string.Format("Unexpected error '{0}' at line {1}", tokens[0].Text, tokens[0].LineNumber));
                    return;
                }
                else
                {
                    Interpreter.Errors.Add("Unexpected token type: " + tokens[0].type);
                }

                expr[expr.Count - 1].Parse(tokens);
            }

        }

        public void Execute(LinePainter painter)
        {
            if (Errors.Count > 0)
            {
                return;
            }

            Debug.WriteLine("Running...");
            foreach (var epxression in expr)
            {
                epxression.Run(painter);
            }
        }
    }
}

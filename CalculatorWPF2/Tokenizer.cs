using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CalculatorWPF
{
    internal class Tokenizer
    {
        //static Regex wholeRegex = new Regex(@"^[a-z]|\d.\d?|\+|\-|\*|\/|\^|\(|\)$");
        static Regex wholeRegex = new Regex(@"(\d+\.\d+|\(|[a-z]|\d+|\)|\+|\-|\/|\*|\^|$)");
        static Regex variableRegex = new Regex(@"^([a-z])$");
        static Regex numberRegex = new Regex(@"^(\d+(.\d+)?)$");
        static Regex weakOperatorRegex = new Regex(@"^(\+|\-)");
        static Regex strongOperatorRegex = new Regex(@"^(\*|\/)$");
        static Regex strongestOperatorRegex = new Regex(@"^(\^)$");
        static Regex bracketRegex = new Regex(@"^(\(|\))$");

        public static List<Token> Tokenize(string mathExpr)
        {
            List<Token> tokens = new List<Token>();
            MatchCollection collection = wholeRegex.Matches(mathExpr);

            foreach (Match m in collection)
            {
                if (m.Value == "")
                    continue;
                Token token = new Token
                {
                    Value = m.Value,
                    Type = m.Value switch
                    {
                        var t when variableRegex.IsMatch(t) => TokenType.Variable,
                        var t when numberRegex.IsMatch(t) => TokenType.Number,
                        var t when weakOperatorRegex.IsMatch(t) => TokenType.WeakOperator,
                        var t when strongOperatorRegex.IsMatch(t) => TokenType.StrongOperator,
                        var t when strongestOperatorRegex.IsMatch(t) => TokenType.StrongestOperator,
                        var t when bracketRegex.IsMatch(t) => TokenType.Bracket,
                        _ => TokenType.Error
                    }
                };
                tokens.Add(token);
            }

            foreach (Token token in tokens)
                Console.WriteLine($"TokenType: {token.Type} Value: {token.Value}");

            return tokens;
        }
    }
}

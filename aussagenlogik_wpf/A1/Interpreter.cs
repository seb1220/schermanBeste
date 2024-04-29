using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace A1
{
    internal class Interpreter
    {
        List<Token> tokens;
        Expression root;
        public Interpreter() { }

        public bool Evaluate(string formel, Dictionary<string, bool> variables)
        {
            Tokenize(formel);
            root = Expression.Parse(tokens);

            return root.Evaluate(variables);
        }

        void Tokenize(string formel)
        {
            Regex baseRegex = new Regex(@"[a-z]+|\(|\)|\∧|\⋁|\↔|\→|\¬");
            Regex variable = new Regex(@"[a-z]+");
            Regex not = new Regex(@"\¬");
            Regex and = new Regex(@"\∧");
            Regex or = new Regex(@"\⋁");
            Regex conditional = new Regex(@"\→");
            Regex biconditional = new Regex(@"\↔");

            tokens = new List<Token>();
            var matches = baseRegex.Matches(formel);

            foreach (Match match in matches)
            {
                Token token = new Token();
                token.Value = match.Value;

                if (not.IsMatch(match.Value)) 
                {
                    token.Type = TokenType.negate;
                } else if (and.IsMatch(match.Value))
                {
                    token.Type = TokenType.and;
                } else if (or.IsMatch(match.Value))
                {
                    token.Type = TokenType.or;
                } else if (conditional.IsMatch(match.Value))
                {
                    token.Type = TokenType.conditional;
                } else if (biconditional.IsMatch(match.Value))
                {
                    token.Type = TokenType.biconditional;
                } else if (variable.IsMatch(match.Value))
                {
                    token.Type = TokenType.varibale;
                } else
                {
                    throw new Exception($"Unexpected Token: {match.Value}");
                }

                tokens.Add(token);
                Console.WriteLine($"Token: {token.Type}");
            }
        }
    }
}

using CalculatorWPF;
using CalculatorWPF.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Parser
{
    public List<Token> tokens;
    public int index;
    public List<string> errors = new List<string>();

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
        this.index = 0;
    }

    public Expression Parse()
    {
        Expression result = AddSub();
        if (index < tokens.Count)
        {
            errors.Add("Unexpected tokens at the end");
        }
        return result;
    }

    private Expression AddSub()
    {
        Expression result = MulDiv() ?? new Literal { Value = DefaultToken(0) };

        while (index < tokens.Count && (tokens[index].Text == "+" || tokens[index].Text == "-"))
        {
            Token op = tokens[index++];
            Expression right = MulDiv() ?? new Literal { Value = DefaultToken(0) };
            result = new PlusMinus { left = result, Operator = op, right = right };
        }

        return result;
    }

    private Expression MulDiv()
    {
        Expression result = Pow() ?? new Literal { Value = DefaultToken(1) };

        while (index < tokens.Count && (tokens[index].Text == "*" || tokens[index].Text == "/" || tokens[index].Text == "%"))
        {
            Token op = tokens[index++];
            Expression right = Pow() ?? new Literal { Value = DefaultToken(1) };
            result = new MalDivMod { left = result, Operator = op, right = right };
        }

        return result;
    }

    private Expression Pow()
    {
        Expression result = Literal() ?? new Literal { Value = DefaultToken(1) };

        while (index < tokens.Count && tokens[index].Text == "^")
        {
            Token op = tokens[index++];
            Expression right = Literal() ?? new Literal { Value = DefaultToken(1) };
            result = new Pow { left = result, Operator = op, right = right };
        }

        return result;
    }

    private Expression Literal()
    {
        if (index >= tokens.Count)
        {
            errors.Add("Unexpected end of expression");
            // return a default value to avoid crashing the program with a token that you have to create

            return new Literal { Value = DefaultToken(1) };
        }
        else if (tokens[index].type == Token.Type.Number)
        {
            return new Literal { Value = tokens[index++] };
        }
        else if (tokens[index].type == Token.Type.Variable)
        {
            return new Variable { Name = tokens[index++] };
        }
        else if (tokens[index].Text == "(")
        {
            index++; // skip '('
            Expression result = AddSub();
            if (index >= tokens.Count || tokens[index].Text != ")")
            {
                errors.Add("Expected ')'");
                return new Literal { Value = DefaultToken(1) };
            }
            index++; // skip ')'
            return result;
        }
        else
        {
            errors.Add("Expected a number, variable, or '('");
            return new Literal { Value = DefaultToken(1) };
        }
    }

    // method that creates default token (for add or sub expression Text = 0 else for other expression param: Text = 1)
    private Token DefaultToken(int type)
    {
        Token token = new Token();
        if (type == 0)
        {
            token.Text = "0";
        }
        else
        {
            token.Text = "1";
        }
        token.type = Token.Type.Number;
        return token;
    }

}

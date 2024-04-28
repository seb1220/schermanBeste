using CalculatorWPF;
using CalculatorWPF.Expressions;
using System;
using System.Collections.Generic;

internal class Parser
{
    public List<Token> tokens;
    public int index;
    public List<string> errors = new List<string>();
    public Expression rootExpression;

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
    }

    public void Parse()
    {
        Console.WriteLine("tokencount: " + tokens.Count);
        rootExpression = new WeakOperator().Parse(tokens);
    }

    public double Evaluate(Dictionary<string, double> variables)
    {
        return rootExpression.Evaluate(variables);
    }
}

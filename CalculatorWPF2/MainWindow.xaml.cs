using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var tokens = Tokenizer.Tokenize("(a+b)*5");
            var parser = new Parser(tokens);
            parser.Parse();
            var variables = new Dictionary<string, double> { { "a", 2 }, { "b", 3 } };
            double result = parser.Evaluate(variables);
            Console.WriteLine($"Das Ergebnis ist: {result}");

            //var tokens = Tokenizer.Tokenize("4.9*a+(z*3)^2");
            //var parser = new Parser(tokens);
            // var variables = new Dictionary<string, double> { { "a", 2 }, { "z", 3 } };
            // double result = parser.Evaluate(variables);
            // Debug.WriteLine($"Das Ergebnis ist: {result}");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string input = button.Content.ToString();
            outputTextBox.Text += input;
        }

        private void butCalc_Click(object sender, RoutedEventArgs e)
        {
            string input = outputTextBox.Text;
            var tokens = Tokenizer.Tokenize(input);
            var parser = new Parser(tokens);
            parser.Parse();

            //get all variables (letters) from input in list
            List<string> variables = new List<string>();
            foreach (Token t in tokens)
            {
                if (t.Type == TokenType.Variable)
                {
                    variables.Add(t.Value);
                }
            }
            // ask for values of variables
            Dictionary<string, double> values = new Dictionary<string, double>();

            foreach (string s in variables)
            {
                //inputbox should be displayed in the middle of the screen
                string value = Microsoft.VisualBasic.Interaction.InputBox
                    ("Please enter a value for " + s,
                    "Value for " + s, "0", (int)SystemParameters.PrimaryScreenWidth / 2,
                    (int)SystemParameters.PrimaryScreenHeight / 2);
                values.Add(s, double.Parse(value));
            }
            var result = parser.Evaluate(values);

            //Output result in textbox
            outputTextBox.Text = result.ToString();

            // Output errors in debug console
            foreach (string s in parser.errors)
            {
                Debug.WriteLine(s);
            }

        }

        private void butC_Click(object sender, RoutedEventArgs e)
        {
            // Get the tokens from the outputTextBox
            var tokens = Tokenizer.Tokenize(outputTextBox.Text);

            // output all tokens to the debug console
            foreach (Token t in tokens)
            {
                Debug.WriteLine(t.Value + " : " + t.Type);
            }

            // Check if there are any tokens
            if (tokens.Count > 0)
            {
                // Get the last token 
                Token lastToken = tokens.LastOrDefault();

                // Remove the last token from the outputTextBox
                outputTextBox.Text = outputTextBox.Text.Remove(outputTextBox.Text.LastIndexOf(lastToken.Value));
            }
            // else remove all tokens from outputTextBox
            else
            {
                outputTextBox.Text = "";
            }
        }

        private void butAC_Click(object sender, RoutedEventArgs e)
        {
            //clear outputTextBox
            outputTextBox.Text = "";
        }
    }
}

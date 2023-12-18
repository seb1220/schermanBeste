using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace roboter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            robotField.LoadField("C:\\Users\\sebas\\source\\repos\\tmpScher\\roboter\\Aufgabe1.xml");
        }

        private void loadMap_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            openFileDialog.InitialDirectory = "C:\\Users\\sebas\\source\\repos\\tmpScher\\roboter";
            if (openFileDialog.ShowDialog() == true)
            {
                robotField.LoadField(openFileDialog.FileName);
            }
        }

        private void loadCode_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TXT files (*.txt)|*.txt";
            openFileDialog.InitialDirectory = "C:\\Users\\sebas\\source\\repos\\tmpScher\\roboter";
            if (openFileDialog.ShowDialog() == true)
            {
                code.SetValue(TextBox.TextProperty, System.IO.File.ReadAllText(openFileDialog.FileName));
            }
        }

        private void run_Click(object sender, RoutedEventArgs e)
        {
            List<Token> tokens = tokanize();

            foreach (Token token in tokens)
            {
                Console.WriteLine(token.value);
            }
            Console.WriteLine("------------------------");

            Program program = new Program(tokens);
            Console.WriteLine("Parsed successfully!");

            foreach (var error in Program.Errors)
                Console.WriteLine(error);

            Console.WriteLine(program.run(robotField));
            Console.WriteLine("Ran successfully!");
        }

        private List<Token> tokanize()
        {
            // Define a regular expression for repeated words.
            Regex rx = new Regex(@"(REPEAT|MOVE|UNTIL|IF|IS-A|COLLECT|UP|DOWN|LEFT|RIGHT|OBSTACLE|\d+|{|}|\S+)(\s+|$)",
              RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Define a test string.
            string text = code.Text;

            // Find matches.
            MatchCollection matches = rx.Matches(text);

            List<Token> tokens = new List<Token>();
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                tokens.Add(new Token(groups[1].Value));
            }

            return tokens;
        }
    }
}

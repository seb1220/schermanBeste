using Microsoft.Win32;
using System;
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

        private void checkSyntax()
        {
            // Define a regular expression for repeated words.
            Regex rx = new Regex(@"(REPEAT|MOVE|UNTIL|IF|IS-A|COLLECT|UP|DOWN|LEFT|RIGHT|OBSTACLE|\d+|{|}|\S+)(\s+|$)",
              RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Define a test string.
            string text = code.Text;

            // Find matches.
            MatchCollection matches = rx.Matches(text);

            // Report the number of matches found.
            Console.WriteLine("{0} matches found in:\n{1}",
                              matches.Count,
                              text);

            // Report on each match.
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                Console.WriteLine("'{0}' repeated at positions {1}",
                                  groups[1].Value,
                                  groups[1].Index);
            }

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
            checkSyntax();
        }
    }
}

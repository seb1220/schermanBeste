using System;
using System.Collections.Generic;
using System.Windows;

namespace A1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Expression parser = null; //mit konkreter Klasse anlegen

            //Variablenliste zurücksetzen
            Expression.variables.Clear();

            //Formel parsen
            parser.ParseFormel(new List<char>(textBox.Text.ToCharArray()));

            //Auswerten für Feld 0001
            bool result = parser.Interpret(getBooleanFromInt(Expression.variables, 1));

            textBox.Text = result.ToString();
        }

        public Dictionary<char, bool> getBooleanFromInt(List<char> variables, int value)
        {
            string binary = Convert.ToString(value, 2).PadLeft(variables.Count, '0');
            if (variables.Count != binary.Length)
                throw new ArgumentException("Not enough variables");
            Dictionary<char, bool> res = new Dictionary<char, bool>();
            for (int i = 0; i < variables.Count; i++)
            {
                res.Add(variables[i], binary[i] == '0' ? false : true);
            }
            return res;
        }
    }
}

using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;

namespace PA2_UE_Interpreter
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

        private void btnLoadMap_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.xml)|*.xml|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;

                robotField.LoadField(filename);
            }
        }

        private void btnLoadCode_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                string[] lines = System.IO.File.ReadAllLines(filename);
                txtCode.Clear();
                foreach (string line in lines)
                {
                    txtCode.Text += line + "\n";
                }
            }
        }

        private void btnExecCode_Click(object sender, RoutedEventArgs e)
        {
            Interpreter interpreter = new Interpreter(robotField);
            interpreter.Tokenize(txtCode.Text);
            interpreter.Parse();
            interpreter.Execute();

            foreach (var error in Interpreter.Errors)
            {
                Debug.WriteLine(error);
            }
        }
    }
}

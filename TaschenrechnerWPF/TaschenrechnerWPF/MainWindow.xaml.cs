using System.Windows;
using System.Windows.Controls;

namespace TaschenrechnerWPF
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

        private void numeric_input(object sender, RoutedEventArgs e)
        {
            string value = (string)((Button)e.OriginalSource).Content;
            if (value == "C")
            {
                Display.Text = "";
            }
            else
            {
                Display.Text += value;
            }
        }

        private void calculate(object sender, RoutedEventArgs e)
        {
            Result.Content = "cool content";
        }

        private void Parse()
        {

        }

        private void Run()
        {

        }
    }
}

using System.Windows;

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

            //robotField.DrawField();
        }
    }
}

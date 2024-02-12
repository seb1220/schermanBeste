using System.Windows;

namespace SchiffeVersenken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal GameField friendlyField;

        public MainWindow()
        {
            InitializeComponent();

            StartDialog startdialog = new StartDialog();

            startdialog.ShowDialog();

            testLabel.Content = startdialog.Columns;

            GameField.Columns = startdialog.Columns;
            GameField.Rows = startdialog.Rows;
            GameField.Ships5 = startdialog.Ships5;
            GameField.Ships4 = startdialog.Ships4;
            GameField.Ships3 = startdialog.Ships3;
            GameField.Ships2 = startdialog.Ships2;

            friendlyField = new GameField();
        }
    }
}

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace SchiffeVersenken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameField FriendlyField { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // this.DataContext = this;

            openStartDialog();

            FriendlyShips.DataContext = FriendlyField;
        }

        private void openStartDialog()
        {
            StartDialog startdialog = new StartDialog();
            startdialog.ShowDialog();

            GameField.setConfig(startdialog.Columns, startdialog.Rows, startdialog.Ships5, startdialog.Ships4, startdialog.Ships3, startdialog.Ships2);
            FriendlyField = new GameField();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            openStartDialog();
        }

        private void FriendlyShips_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Debug.WriteLine(((ListBox)sender).SelectedItem.ToString());
            ((Cell)((ListBox)sender).SelectedItem).IsShip = true;
        }
    }
}

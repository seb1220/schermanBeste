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
        public GameField EnemyField { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            openStartDialog();

            FriendlyShips.DataContext = this;

            Debug.WriteLine(GameField.Size);
            Debug.WriteLine(GameField.Ships5);

            Debug.WriteLine(EnemyField.Field.Count);
            Debug.WriteLine(FriendlyField.Field.Count);
        }

        private void openStartDialog()
        {
            StartDialog startdialog = new StartDialog();
            startdialog.ShowDialog();

            GameField.setConfig(startdialog.Size, startdialog.Ships5, startdialog.Ships4, startdialog.Ships3, startdialog.Ships2);
            FriendlyField = new GameField();
            EnemyField = new GameField();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            openStartDialog();
        }

        private void FriendlyShips_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (FriendlyShips.SelectedIndex == -1)
                return;

            FriendlyField.Field[FriendlyShips.SelectedIndex].toggleShip();
            FriendlyShips.UnselectAll();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void EnemyShips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

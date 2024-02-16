using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace SchiffeVersenken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameField FriendlyField { get; set; }
        public ObservableCollection<Cell> Field { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            Field = new ObservableCollection<Cell>();
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    Field.Add(new Cell(new Point(i, j)));
                }
            }

            StartDialog startdialog = new StartDialog();

            startdialog.ShowDialog();

            testLabel.Content = startdialog.Columns;

            GameField.Columns = startdialog.Columns;
            GameField.Rows = startdialog.Rows;
            GameField.Ships5 = startdialog.Ships5;
            GameField.Ships4 = startdialog.Ships4;
            GameField.Ships3 = startdialog.Ships3;
            GameField.Ships2 = startdialog.Ships2;

            FriendlyField = new GameField();

            Debug.WriteLine(FriendlyField.Field.Count);
        }
    }
}
